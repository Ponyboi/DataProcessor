using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CommunityCoveoDataProcessor.Model
{
    public class TelligentData
    {
        public string uri { get; set; }
        public string binaryData { get; set; }
        public Dictionary<string, string> meta { get; set; }
        public JObject permissions { get; set; }

        public Task<List<TelligentData>> GetData(int pageNum = 0) {
            return new Task<List<TelligentData>>(() => { return new List<TelligentData>(); });
        }
    }
}
