using CommunityCoveoDataProcessor.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityCoveoDataProcessor.Json
{
    public class OrganizationConverter : JsonCreationConverter<OrganizationData>
    {
        protected override OrganizationData Create(Type objectType, JObject jObject)
        {
            return new OrganizationData();
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }

        protected override JObject ConvertJson(JObject jObject)
        {
            //Binary data search content
            string binaryData = String.Format("{0} {1} {2} {3}",
                (string)jObject["Author"]["DisplayName"],
                (string)jObject["Date"],
                (string)jObject["Subject"],
                (string)jObject["Body"]
                );
            binaryData = Convert.ToBase64String(Encoding.UTF8.GetBytes(binaryData));

            //Meta data
            //Dictionary<string, string> metaData = new Dictionary<string, string>
            //{
            //    {"IsFeatured", (string)jObject["IsFeatured"]},
            //    {"ForumId", (string)jObject["ForumId"]},
            //    {"GroupId", (string)jObject["GroupId"]},
            //    {"ThreadType", (string)jObject["ThreadType"]},
            //    {"Date", (string)jObject["Date"]},
            //    {"Url", (string)jObject["Url"]},
            //    {"ViewCount", (string)jObject["ViewCount"]},
            //    {"Body", (string)jObject["Body"]},
            //    {"Subject", (string)jObject["Subject"]},
            //    {"AuthorName", (string)jObject["Author"]["DisplayName"]}
            //};

            //Permissions
            List<KeyValuePair<string, string>> allowed = new List<KeyValuePair<string, string>>
            {
              new KeyValuePair<string, string>("name", "user1"),
              new KeyValuePair<string, string>("name", "user2"),
              new KeyValuePair<string, string>("name", "group1"),
            };
            string allowedJson = JsonConvert.SerializeObject(allowed, Formatting.None, new KeyValueResolver());
            JArray jAllowed = JArray.Parse(allowedJson);

            List<KeyValuePair<string, string>> denied = new List<KeyValuePair<string, string>>
            {

            };
            string deniedJson = JsonConvert.SerializeObject(denied, Formatting.None, new KeyValueResolver());
            JArray jDenied = JArray.Parse(deniedJson);

            JObject perms = new JObject();
            perms.Add("allowed", jAllowed);
            perms.Add("denied", jDenied);
            string permsJson = JsonConvert.SerializeObject(perms, Formatting.None, new KeyValueResolver());

            JObject jPerms = JObject.Parse(permsJson);
            JObject permissions = new JObject();
            permissions.Add("permissions", jPerms);

            string metaDataJson = JsonConvert.SerializeObject(metaData, Formatting.None);
            JObject jMeta = JObject.Parse(metaDataJson);

            jObject.Add("binaryData", binaryData);
            jObject.Add("meta", jMeta);
            jObject.Add("uri", (string)jObject["Url"]);
            jObject.Add("permissions", jPerms);



            return jObject;
        }
    }
}
