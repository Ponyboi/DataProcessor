using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityCoveoDataProcessor.Model
{
    public class OrganizationData : TelligentData
    {
        public string ContentId { get; set; }
        public string ContentTypeId { get; set; }
        public int Id { get; set; }
    }
}
