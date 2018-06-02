using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FTEReader.WebRequest
{
    //表示章节详情的对象，用于调用api返回的json文件的序列化
    class ChapterDetail
    {
        public async static Task<ChapterDetailObject> GetChapterDetail(string link)
        {
            ChapterDetailObject data = null;
            try
            {
                var http = new HttpClient();
                string url = "http://chapter2.zhuishushenqi.com/chapter/" + link;
                //string url = "http://chapter2.zhuishushenqi.com/chapter/http:%2F%2Fbook.my716.com%2FgetBooks.aspx%3Fmethod=content&bookId=633074&chapterFile=U_753547_201607012243065574_6770_1.txt";
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(ChapterDetailObject));

                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                data = (ChapterDetailObject)serializer.ReadObject(ms);
            }
            catch (Exception e)
            {

            }
            return data;

        }
    }
    public class chapter
    {
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 在晋国都城新绛数里之外，耸立着一座夯土墙环绕的坚固小城，此城名为赵氏之宫，乃是晋国六大卿族之一，赵氏的私邑。
   
        /// </summary>
        public string body { get; set; }
}

public class ChapterDetailObject
{
    /// <summary>
    /// 
    /// </summary>
    public string ok { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public chapter chapter { get; set; }
}
}
