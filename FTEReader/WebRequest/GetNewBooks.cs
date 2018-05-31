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
    class GetNewBooks
    {
        public async static Task<BooksObject> GetNewBook(string type, string major, string tag,string start,string limit,string gender)
        {   
            BooksObject data = null;
            try
            {
                var http = new HttpClient();
                String url = "http://api.zhuishushenqi.com/book/by-categories?gender="+gender+"&type="+type+"&major="+major+"&minor="+tag+"&start="+start+"&limit="+limit;
                
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(BooksObject));

                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                data = (BooksObject)serializer.ReadObject(ms);
            }
            catch (Exception e)
            {
                
            }
            return data;

        }
    }

    [DataContract]
    public class BooksItem
    {
        /// <summary>
        /// 书的序列id（非常重要，后续获得章节及章节内容有用到）
        /// </summary>
        [DataMember]
        public string _id { get; set; }
        /// <summary>
        /// 书的题目：圣墟
        /// </summary>
        [DataMember]
        public string title { get; set; }
        /// <summary>
        /// 书的作者：辰东
        /// </summary>
        [DataMember]
        public string author { get; set; }
        /// <summary>
        /// 书的短简介：
        /// 在破败中崛起，在寂灭中复苏。沧海成尘，雷电枯竭，那一缕幽雾又一次临近大地，世间的枷锁被打开了，
        /// 一个全新的世界就此揭开神秘的一角……
        /// </summary>
        [DataMember]
        public string shortIntro { get; set; }
        /// <summary>
        /// 书的封面地址，图片域名: pictureAddress= "http://statics.zhuishushenqi.com"
        /// 所以获得图片的地址为： address = pictureAddress + cover;
        /// </summary>
        [DataMember]
        public string cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string site { get; set; }
        /// <summary>
        /// 玄幻
        /// </summary>
        [DataMember]
        public string majorCate { get; set; }
        /// <summary>
        /// 东方玄幻
        /// </summary>
        [DataMember]
        public string minorCate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int sizetype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string superscript { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string contentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string allowMonthly { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int banned { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int latelyFollower { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public double retentionRatio { get; set; }
        /// <summary>
        /// 第1073章 于往生中回归
        /// </summary>
        [DataMember]
        public string lastChapter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<string> tags { get; set; }
    }

    [DataContract]
    public class BooksObject
    {
        /// <summary>
        /// 这个类型的书（major/tag）总的数量
        /// </summary>
        [DataMember]
        public int total { get; set; }
        /// <summary>
        /// 书本信息的List表
        /// </summary>
        [DataMember]
        public List<BooksItem> books { get; set; }
        /// <summary>
        /// API查询结果
        /// </summary>
        [DataMember]
        public string ok { get; set; }
    }
}
