using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityCoveoDataProcessor.Model
{
    public class ResponseData
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public List<object> Info { get; set; }
        public List<object> Warnings { get; set; }
        public List<object> Errors { get; set; }
    }
}
