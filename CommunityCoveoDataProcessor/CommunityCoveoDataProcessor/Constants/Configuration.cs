using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CommunityCoveoDataProcessor.Constants
{
    public static class Configuration
    {
        public static class TelligentAPI
        {
            //public const string baseApi = { get { if ()}
            public static string ClientId { get { return ConfigurationManager.AppSettings[String.Format(Constants.ClientId, ConfigurationManager.AppSettings["TelligentAPI.Target"])]; } }
            public static string ClientSecret { get { return ConfigurationManager.AppSettings[String.Format(Constants.ClientSecret, ConfigurationManager.AppSettings["TelligentAPI.Target"])]; } }
            public static string CommunityBaseUrl
            { get
                {
                    return ConfigurationManager.AppSettings[String.Format(Constants.CommunityBaseUrl, ConfigurationManager.AppSettings["TelligentAPI.Target"])];
                }
            }
            public static string AdminAPIKey { get { return ConfigurationManager.AppSettings[String.Format(Constants.AdminAPIKey, ConfigurationManager.AppSettings["TelligentAPI.Target"])]; } }

            public static string AdminUsername { get { return ConfigurationManager.AppSettings["TelligentAPI.AdminUsername"]; } }
            public static string RestHeader { get { return ConfigurationManager.AppSettings["TelligentAPI.RestHeader"]; } }

            //
        }
        public static class Processor
        {
            public static string Delay { get { return ConfigurationManager.AppSettings["Processor.Delay"]; } }
            
        }
    }
}
