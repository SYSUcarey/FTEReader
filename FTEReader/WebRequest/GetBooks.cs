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
    class GetBooks
    {
        public async static Task<RootObject> GetBook(int catalogId, int pn, int rn)
        {
            RootObject data = null;
            try
            {
                var http = new HttpClient();
                String url = "http://apis.juhe.cn/goodbook/query?key=bc272810070251cd25601306026f0370&catalog_id=" + catalogId + "&pn=" + pn +  "&rn=" + rn;
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(RootObject));

                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                data = (RootObject)serializer.ReadObject(ms);
            }
            catch (Exception e)
            {

            }
            return data;

        }
    }
    [DataContract]
    public class Datum
    {
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string catalog { get; set; }
        [DataMember]
        public string tags { get; set; }
        [DataMember]
        public string sub1 { get; set; }
        [DataMember]
        public string sub2 { get; set; }
        [DataMember]
        public string img { get; set; }
        [DataMember]
        public string reading { get; set; }
        [DataMember]
        public string online { get; set; }
        [DataMember]
        public string bytime { get; set; }
    }

    public class Result
    {
        [DataMember]
        public List<Datum> data { get; set; }
        [DataMember]
        public string totalNum { get; set; }
        [DataMember]
        public int pn { get; set; }
        [DataMember]
        public string rn { get; set; }
    }

    public class RootObject
    {
        [DataMember]
        public string resultcode { get; set; }
        [DataMember]
        public string reason { get; set; }
        [DataMember]
        public Result result { get; set; }
    }
}
