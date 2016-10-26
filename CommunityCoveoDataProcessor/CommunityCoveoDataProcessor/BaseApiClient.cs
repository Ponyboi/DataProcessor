using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommunityCoveoDataProcessor
{
    public abstract class BaseApiClient
    {
        public Uri ServerUri { get; protected set; }

        public Uri CoveoUri { get; protected set; }

        public string ApiToken { get; protected set; }

        public string AdminName { get; protected set; }

        public string CoveoApiToken { get; protected set; }

        public int ApiMaxDegreesParallelism { get; set; }

        protected Uri GetRestUri(string uri, Guid? id = null, string sku = null, int? upc = null, int? pageIndex = null, int? pageSize = null, bool? extended = null)
        {
            var qs = new Dictionary<string, string>();

            if (id.HasValue)
            {
                qs.Add("id", id.ToString());
            }

            if (!string.IsNullOrEmpty(sku))
            {
                qs.Add("sku", sku);
            }

            if (upc.HasValue)
            {
                qs.Add("upc", upc.ToString());
            }

            if (pageIndex.HasValue)
            {
                qs.Add("pageIndex", pageIndex.ToString());
            }

            if (pageSize.HasValue)
            {
                qs.Add("pageSize", pageSize.ToString());
            }

            if (extended.HasValue)
            {
                qs.Add("extended", extended.ToString());
            }

            return GetRestUri(uri, qs);
        }

        protected Uri GetRestUri(string relativeUri, IDictionary<string, string> queryParameters)
        {
            var baseUri = ServerUri;
            var builder = new UriBuilder(new Uri(baseUri, relativeUri));
            var qs = HttpUtility.ParseQueryString(builder.Query);

            foreach (var param in queryParameters)
            {
                qs[param.Key] = param.Value;
            }

            builder.Query = qs.ToString();

            return builder.Uri;
        }

        protected Uri GetCoveoRestUri(string relativeUri, IDictionary<string, string> queryParameters)
        {
            return new Uri(Constants.Constants.basePushApi);
        }
    }
}
