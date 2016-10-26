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
        public const string baseApi = "https://tessituracommunity9.hq.bluetubeinc.com/api.ashx/v2/";
        public const string authToken = "fbfb0af1cc4a4d6aa6ba185130bd40f8a2bcbfc1cb7b4cdd9660128da85d95de";
        public const string adminName = "toddlantry3834";
        public const string adminApiToken = "hedzm25chnxr8uusmqy1jg40o8976tdh";
        public const string authHeaderKey = "Rest-User-Token";

        //Coveo
        public const string basePushApi = "http://localhost:8080/rest/push/v1/documents";
        public const string basePushApiSecretHeader = "Coveo-Push-Secret";
        public const string basePushApiSecret = "testing";
    }
}
