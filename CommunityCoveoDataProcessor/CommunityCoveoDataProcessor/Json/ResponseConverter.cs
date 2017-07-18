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
    public class ResponseConverter : JsonCreationConverter<ResponseData>
    {
        protected override ResponseData Create(Type objectType, JObject jObject)
        {
            return new ResponseData();
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }

        protected override JObject ConvertJson(JObject jObject)
        {
            JObject newJObject = new JObject();
            newJObject.Add("PageSize", (string)jObject["PageSize"]);
            newJObject.Add("PageIndex", (string)jObject["PageIndex"]);
            newJObject.Add("TotalCount", (string)jObject["TotalCount"]);
            //newJObject.Add("Info", JsonConvert.DeserializeObject<List<object>>(jObject["Info"]);
            //newJObject.Add("Warnings", (List<object>)jObject["Warnings"]);
            //newJObject.Add("Errors", (List<object>)jObject["Errors"]);

        //string deniedJson = JsonConvert.SerializeObject(denied, Formatting.None, new KeyValueResolver());
        //    JArray jDenied = JArray.Parse(deniedJson);

            return newJObject;
        }
    }
}
