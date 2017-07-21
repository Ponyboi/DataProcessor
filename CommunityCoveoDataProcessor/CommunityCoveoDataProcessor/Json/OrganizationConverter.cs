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
            JObject newJObject = new JObject();
            newJObject.Add("Id", (string)jObject["Id"]);

            return newJObject;
        }
    }
}
