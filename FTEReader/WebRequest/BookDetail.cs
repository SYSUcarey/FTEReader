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
    class BookDetail
    {
        public async static Task<BookDetailObject> GetBookDetail(string bookid)
        {
            BookDetailObject data = null;
            try
            {
                var http = new HttpClient();
                string url = "http://api.zhuishushenqi.com/book/" + bookid;
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(BookDetailObject));

                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                data = (BookDetailObject)serializer.ReadObject(ms);
            }
            catch (Exception e)
            {

            }
            return data;

        }
    }

    [DataContract]
    public class Rating
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public double score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string isEffect { get; set; }
    }

    public class BookDetailObject
    {
        /// <summary>
        /// 书的序列id：bookid
        /// </summary>
        [DataMember]
        public string _id { get; set; }
        /// <summary>
        /// 圣墟
        /// </summary>
        [DataMember]
        public string title { get; set; }
        /// <summary>
        /// 辰东
        /// </summary>
        [DataMember]
        public string author { get; set; }
        /// <summary>
        /// 在破败中崛起，在寂灭中复苏。沧海成尘，雷电枯竭，那一缕幽雾又一次临近大地，世间的枷锁被打开了，一个全新的世界就此揭开神秘的一角……
        /// </summary>
        [DataMember]
        public string longIntro { get; set; }
        /// <summary>
        /// 书籍封面图片
        /// </summary>
        [DataMember]
        public string cover { get; set; }
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
        
        [DataMember]
        public string creater { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<string> hiddenPackage { get; set; }
        
        [DataMember]
        public List<int> apptype { get; set; }
        
        [DataMember]
        public Rating rating { get; set; }
        
        [DataMember]
        public string hasCopyright { get; set; }
        
        [DataMember]
        public int buytype { get; set; }
        
        [DataMember]
        public int sizetype { get; set; }
        
        [DataMember]
        public string superscript { get; set; }
        
        [DataMember]
        public int currency { get; set; }
        
        [DataMember]
        public string contentType { get; set; }
        
        [DataMember]
        public string _le { get; set; }
        
        [DataMember]
        public string allowMonthly { get; set; }
        
        [DataMember]
        public string allowVoucher { get; set; }
        
        [DataMember]
        public string allowBeanVoucher { get; set; }
        
        [DataMember]
        public string hasCp { get; set; }
        
        [DataMember]
        public int postCount { get; set; }
        
        [DataMember]
        public int latelyFollower { get; set; }
        
        [DataMember]
        public int followerCount { get; set; }
        
        [DataMember]
        public int wordCount { get; set; }
        
        [DataMember]
        public int serializeWordCount { get; set; }
        
        [DataMember]
        public string retentionRatio { get; set; }
        
        [DataMember]
        public string updated { get; set; }
        
        [DataMember]
        public string isSerial { get; set; }
        
        [DataMember]
        public int chaptersCount { get; set; }
        /// <summary>
        /// 第1074章 沉沦
        /// </summary>
        [DataMember]
        public string lastChapter { get; set; }
        
        [DataMember]
        public List<string> gender { get; set; }
        
        [DataMember]
        public List<string> tags { get; set; }

        [DataMember]
        public string advertRead { get; set; }
        /// <summary>
        /// 东方玄幻
        /// </summary>
        [DataMember]
        public string cat { get; set; }

        [DataMember]
        public string donate { get; set; }
        /// <summary>
        /// 阅文集团正版授权
        /// </summary>
        [DataMember]
        public string copyright { get; set; }

        [DataMember]
        public string _gg { get; set; }

        [DataMember]
        public string discount { get; set; }

        [DataMember]
        public string limit { get; set; }
    }
}
