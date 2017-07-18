using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityCoveoDataProcessor.Constants
{
    public static class Constants
    {
        //Telligent
        public const string baseApi = "http://community.tessituranetwork.com/api.ashx/v2/";
        public const string authToken = "a25ldDQ1ZzM4dWhqbjp0b2RkbGFudHJ5MzgzNA==";
        public const string adminName = "toddlantry3834";
        public const string adminApiToken = "knet45g38uhjn";
        public const string authHeaderKey = "Rest-User-Token";

        public const string ClientId = "TelligentAPI.{0}.ClientId";
        public const string ClientSecret = "TelligentAPI.{0}.ClientSecret";
        public const string CommunityBaseUrl = "TelligentAPI.{0}.CommunityBaseUrl";
        public const string AdminAPIKey = "TelligentAPI.{0}.AdminAPIKey";

        //Coveo
        public const string basePushApi = "http://localhost:8080/rest/push/v1/documents";
        public const string basePushApiSecretHeader = "Coveo-Push-Secret";
        public const string basePushApiSecret = "testing";
    }
}
