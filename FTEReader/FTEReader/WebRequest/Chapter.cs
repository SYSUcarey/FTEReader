using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FTEReader.WebRequest
{
    //表示章节的对象，用于调用api返回的jspn文件的序列化
    class Chapter
    {
        public async static Task<ChapterObject> GetChapter(string bookid)
        {
            ChapterObject data = null;
            try
            {
                var http = new HttpClient();
                string url = "http://api.zhuishushenqi.com/mix-atoc/" + bookid + "?view=chapters";
                //string url = "http://api.zhuishushenqi.com/mix-atoc/5569ba444127a49f1fa99d29?view=chapters";
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(ChapterObject));

                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                data = (ChapterObject)serializer.ReadObject(ms);
            }
            catch (Exception e)
            {

            }
            return data;

        }
    }
    [DataContract]
    public class ChaptersItem
    {
        /// <summary>
        /// 真正的有用的link
        /// </summary>
        [DataMember]
        public string link { get; set; }
        /// <summary>
        /// 第1章 家族黑历史
        /// </summary>
        [DataMember]
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string unreadble { get; set; }
    }

    [DataContract]
    public class MixToc
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string _id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string book { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int chaptersCount1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string chaptersUpdated { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<ChaptersItem> chapters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string updated { get; set; }
    }

    [DataContract]
    public class ChapterObject
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public MixToc mixToc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ok { get; set; }
    }
}
