using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.OAuthClient.Version1;
using Telligent.Evolution.Extensibility.Rest.Version1;
using CommunityCoveoDataProcessor.Constants;
using Newtonsoft.Json.Linq;

namespace CommunityCoveoDataProcessor.Model
{
    public class ForumData : TelligentData
    {
        public string ContentId { get; set; }
        public string ContentTypeId { get; set; }
        public string Id { get; set; }
        //public string IsFeatured { get; set; }
        //public string ForumId { get; set; }
        //public string GroupId { get; set; }
        //public string ThreadType { get; set; }
        //public string Date { get; set; }
        //public string Url { get; set; }
        //public string ViewCount { get; set; }
        //public string Body { get; set; }
        //public string BodyData { get; set; }
        //public string Subject { get; set; }
        //public string AuthorName { get; set; }
        //public string[] Tags { get; set; }

        public ForumData()
        {

        }


        public Task<List<TelligentData>> GetData(int pageNum = 0)
        {
            return new Task<List<TelligentData>>(() => { return new List<TelligentData>(); });
        }

        public Task<List<ForumData>> GetDatas(int pageNum = 0)
        {
            Task<List<ForumData>> data = null;

            try
            {
                ///var host = Host.Get("default");
                //var options = new NameValueCollection();
                //options.Add("PageSize", "50");
                //options.Add("PageIndex", "0");
                //options.Add("SortBy", "LastPost");
                //options.Add("SortOrder", "Descending");

                //RestGetOptions getOptions = new RestGetOptions();
                //getOptions.QueryStringParameters = options;
                //var response = host.GetToDynamic(2, req);//, false, getOptions);

                //data = JsonConvert.DeserializeObjectAsync<Task<List<ForumData>>>(response);
                // string json = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);

                //using (var client = GetRestClient(ServerUri, ApiToken))
                //{
                //    var webClient = new WebClient();
                //    var adminKey = String.Format("{0}:{1}", "hedzm25chnxr8uusmqy1jg40o8976tdh", "toddlantry3834");
                //    var adminKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(adminKey));

                //    webClient.Headers.Add("Rest-User-Token", adminKeyBase64);
                //    var requestUrl = "http://tessituracommunity9.hq.bluetubeinc.com/api.ashx/v2/abuseappeals.json";// Constants.Constants.baseApi + "Threads.json";
                //    requestUrl += "?PageSize=100&PageIndex=" + pageNum;
                //    var response = webClient.DownloadString(requestUrl);
                //    data = JsonConvert.DeserializeObjectAsync<Task<List<ForumData>>>(response);
                //}
            }
            catch (Exception e)
            {

            }

            return data;
        }

        
    }
}
