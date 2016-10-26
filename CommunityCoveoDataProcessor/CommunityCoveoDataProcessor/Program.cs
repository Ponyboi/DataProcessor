using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tessitura.Website.Framework.Mediators;

namespace CommunityCoveoDataProcessor
{
    public class Program
    {
        static void Main(string[] args)
        {
            //TelligentMediator mediator = new TelligentMediator();
            //mediator.ProcessForumData();

            TelligentApiClient client = new TelligentApiClient(new Uri(Constants.Constants.baseApi),
                                                               new Uri(Constants.Constants.basePushApi),
                                                                       Constants.Constants.basePushApiSecret,
                                                                       Constants.Constants.adminApiToken,
                                                                       Constants.Constants.adminName);
            client.ProcessForumData();
        }
    }
}
