using CommunityCoveoDataProcessor.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using CommunityCoveoDataProcessor.Json;
using System.Collections.Specialized;
using System.Net;
using SystemTask = System.Threading.Tasks.Task;
using Telligent.Evolution.Extensibility.Rest.Version1;
using CommunityCoveoDataProcessor.Constants;

namespace CommunityCoveoDataProcessor
{
    public class TelligentApiClient : BaseApiClient
    {
        public TelligentApiClient(Uri serverUri, Uri coveoUri, string apiToken, string coveoApiToken, string adminName)
        {
            this.ServerUri = serverUri;
            this.CoveoUri = coveoUri;
            this.CoveoApiToken = coveoApiToken;
            this.ApiToken = apiToken;
            this.AdminName = adminName;
        }

        public string MethodTest(string req)
        {
            var host = Telligent.Evolution.Extensibility.Rest.Version1.Host.Get("default");
            var response = host.GetToDynamic(2, req);
            string json = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public void ProcessForumData()
        {
            try
            {
                Console.WriteLine("ProcessForumData init");
                var getDataBlock = new TransformBlock<int, List<ForumData>>(
                    async i =>
                    {
                        var queryParams = new Dictionary<string, string>
                        {
                            {"PageSize", "100"},
                            {"PageIndex", i.ToString()}
                        };
                        //ForumData repo = new ForumData();
                        //return await repo.GetDatas(i);

                        using (var client = GetTelligentRestClient(ServerUri, ApiToken))
                        {
                            var response = (await client
                                    .GetAsync(GetRestUri("roles/16/users.json", queryParams))
                                    .ConfigureAwait(false))
                                    .EnsureSuccessStatusCode();
                            string result = response.Content.ReadAsStringAsync().Result;
                            JObject jsonResult = JObject.Parse(result);
                            string jsonTarget = jsonResult.Properties().Where(p => p.Name == "Threads").FirstOrDefault().Value.ToString();
                            var content = (await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<ForumData>>(jsonTarget, new ForumsConverter())));
                            //.ConfigureAwait(false));
                            //.EnsureSuccessServiceResponse();
                            // TODO: Use a service response class
                            Task<List<ForumData>> test = Task.FromResult(content);

                            return await Task.FromResult(content);
                        }
                    }, new ExecutionDataflowBlockOptions
                    {
                        MaxDegreeOfParallelism = 1
                    });

                var writeDataBlock = new ActionBlock<List<ForumData>>(
                    async c =>
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.ContractResolver = new DictionaryAsArrayResolver();

                        var queryParams = new Dictionary<string, string>
                        {

                        };

                        //string json = JsonConvert.SerializeObject(c, settings);
                        Dictionary<string, List<ForumData>> documents = new Dictionary<string, List<ForumData>>
                        {
                            { "documents", c }
                        };
                        string json = JsonConvert.SerializeObject(documents, Formatting.None);
                        //JObject pay = JObject.FromObject(c);
                        //JObject load = new JObject();
                        //load.Add("documents", pay);
                        var payload = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var client = GetCoveoRestClient(CoveoUri, ApiToken))
                        {
                            var response = (await client
                                    .PostAsync(GetCoveoRestUri("", queryParams), payload)
                                    .ConfigureAwait(false));
                            string result = response.Content.ReadAsStringAsync().Result;
                            Console.WriteLine(result);
                            await Task.Delay(5000);
                        }
                        //using (var client = GetCoveoRestClient(CoveoUri, ApiToken))
                        //{
                        //    var response = (await client
                        //            .PostAsync(GetCoveoRestUri("", queryParams), payload)
                        //            .ConfigureAwait(false));
                        //    string result = response.Content.ReadAsStringAsync().Result;
                        //    Console.WriteLine(result);
                        //    await Task.Delay(5000);
                        //}

                        //string json = JsonConvert.SerializeObject(obj, settings);

                        Console.WriteLine(c.FirstOrDefault().ContentId);
                    });
                getDataBlock.LinkTo(
                    writeDataBlock, new DataflowLinkOptions
                    {
                        PropagateCompletion = true
                    });

                int count = 6;
                for (int id = 0; id < count; id++)
                {
                    getDataBlock.Post(id);
                }

                getDataBlock.Complete();
                writeDataBlock.Completion.Wait();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae.Message);
                string test = "";
                System.Threading.Thread.Sleep(5000);
            }
        }

        public void ProcessOrganizations()
        {
            try
            {
                string endpoint = "";
                ResponseData responseData = SystemTask.Run(() => GetInitialRequest(endpoint)).Result;

                    Console.WriteLine("ProcessOrganizationsData init");
                    var getDataBlock = new TransformBlock<int, List<OrganizationData>>(
                    async i =>
                    {
                        var queryParams = new Dictionary<string, string>
                        {
                            {"PageSize", "100"},
                            {"PageIndex", i.ToString()}
                        };
                        //ForumData repo = new ForumData();
                        //return await repo.GetDatas(i);

                        using (var client = GetTelligentRestClient(ServerUri, ApiToken))
                        {
                            var response = (await client
                                    .GetAsync(GetRestUri("roles/16/users.json", queryParams))
                                    .ConfigureAwait(false))
                                    .EnsureSuccessStatusCode();
                            string result = response.Content.ReadAsStringAsync().Result;
                            JObject jsonResult = JObject.Parse(result);
                            string jsonTarget = jsonResult.Properties().Where(p => p.Name == "Users").FirstOrDefault().Value.ToString();
                            var content = (await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<OrganizationData>>(jsonTarget, new OrganizationConverter())));
                            //.ConfigureAwait(false));
                            //.EnsureSuccessServiceResponse();
                            // TODO: Use a service response class
                            Task<List<OrganizationData>> test = Task.FromResult(content);

                            return await Task.FromResult(content);
                        }
                    }, new ExecutionDataflowBlockOptions
                    {
                        MaxDegreeOfParallelism = 1
                    });

                var writeDataBlock = new ActionBlock<List<OrganizationData>>(
                    async c =>
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.ContractResolver = new DictionaryAsArrayResolver();

                        var queryParams = new Dictionary<string, string>
                        {

                        };

                        //string json = JsonConvert.SerializeObject(c, settings);
                        Dictionary<string, List<OrganizationData>> documents = new Dictionary<string, List<OrganizationData>>
                        {
                            { "documents", c }
                        };
                        string json = JsonConvert.SerializeObject(documents, Formatting.None);
                        //JObject pay = JObject.FromObject(c);
                        //JObject load = new JObject();
                        //load.Add("documents", pay);
                        var payload = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var WebClient = new WebClient())
                        {
                            foreach (OrganizationData org in c)
                            {
                                string apiEndPoint = String.Format("http://qa.tessituranetwork.com/Community/api.ashx/v2/users/{0}.xml", org.Id);

                                WebClient.Headers.Add("Rest-User-Token", "ZjJ0a3AwbHR0azZudzp0b2RkbGFudHJ5MzgzNA==");

                                WebClient.Headers.Add("Rest-Method", "PUT");

                                var values = new NameValueCollection();

                                values.Add("_ProfileFields_TessituraOrganization", "True");

                                WebClient.UseDefaultCredentials = true;

                                WebClient.UploadValuesAsync(GetRestUri(apiEndPoint, queryParams), values);
                            }
                            await Task.Delay(1000);
                        }

                        //using (var client = GetCoveoRestClient(CoveoUri, ApiToken))
                        //{
                        //    var response = (await client
                        //            .PostAsync(GetCoveoRestUri("", queryParams), payload)
                        //            .ConfigureAwait(false));
                        //    string result = response.Content.ReadAsStringAsync().Result;
                        //    Console.WriteLine(result);
                        //    await Task.Delay(5000);
                        //}

                        //string json = JsonConvert.SerializeObject(obj, settings);

                        Console.WriteLine(c.FirstOrDefault().ContentId);
                    });
                getDataBlock.LinkTo(
                    writeDataBlock, new DataflowLinkOptions
                    {
                        PropagateCompletion = true
                    });

                int count = responseData.TotalCount;
                for (int id = 0; id < count; id++)
                    getDataBlock.Post(id);

                getDataBlock.Complete();
                writeDataBlock.Completion.Wait();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae.Message);
                string test = "";
                System.Threading.Thread.Sleep(5000);
            }
        }

        public async Task<ResponseData> GetInitialRequest(string endpoint)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ResponseData responseData = null;
            var host = new ClientCredentialsRestHost(Configuration.TelligentAPI.Username, Configuration.TelligentAPI.CommunityBaseUrl, Configuration.TelligentAPI.ClientId, Configuration.TelligentAPI.ClientSecret);
            //var response = await host.GetToStringAsync(2, "user.json", false, null );
            var response = await host.GetToStringAsync(2, "user.json", false, new RestGetOptions
            {
                QueryStringParameters = new System.Collections.Specialized.NameValueCollection {
                  {"PageSize", "100"}
                }
            });
            if (response != null)
            {
                ResponseData tRespsonse = JsonConvert.DeserializeObject<ResponseData>(response);
                responseData = tRespsonse;
            }
            return responseData;
        }

        protected HttpClient GetTelligentRestClient(Uri baseAddress, string apiToken)
        {
            var client = new HttpClient();

            var adminKey = String.Format("{0}:{1}", Constants.Constants.adminApiToken, Constants.Constants.adminName);
            var adminKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(adminKey));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.authHeaderKey, adminKeyBase64);
            client.BaseAddress = baseAddress;
            //client.DefaultRequestHeaders.Add("X-API-Token", apiToken);
            client.DefaultRequestHeaders.Add(Constants.Constants.authHeaderKey, adminKeyBase64);

            return client;
        }

        protected HttpClient GetCoveoRestClient(Uri baseAddress, string apiToken)
        {
            var client = new HttpClient();

            client.BaseAddress = baseAddress;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(Constants.Constants.basePushApiSecretHeader, apiToken);
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            return client;
        }


    }
}
