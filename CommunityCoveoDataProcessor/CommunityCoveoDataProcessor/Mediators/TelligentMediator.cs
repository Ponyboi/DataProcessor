using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading.Tasks.Dataflow;
using CommunityCoveoDataProcessor.Model;

namespace Tessitura.Website.Framework.Mediators
{
    public class TelligentMediator
    {
        public void RequestTelligentAuthorization()
        {
        }

        public string MethodTest(string req)
        {
            var host = Telligent.Evolution.Extensibility.Rest.Version1.Host.Get("default");
            var response = host.GetToDynamic(2, req);
            string json = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);

            return json;
        }
    }
}
  