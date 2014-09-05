using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.Core.Components
{
    public class RequestDataSource : IRequestDataSource
    {
        private string[] _splittedRequest;
        private string _requestString;

        public void SetRequestString(string requestString)
        {
            _requestString = requestString;
            _splittedRequest = requestString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string GetRequestType()
        {
            var rt = _splittedRequest.First();
            return rt.Substring(0, rt.IndexOf(" ", StringComparison.Ordinal)).ToUpperInvariant();
        }

        public NameValueCollection ExtractPostData()
        {
            return HttpUtility.ParseQueryString(_splittedRequest.Last());
        }

        public NameValueCollection ExtractGetData()
        {
            var paramString = _splittedRequest[0];
            int iqs = paramString.IndexOf('?');
            if (iqs >= 0)
            {
                var end = paramString.LastIndexOf(" HTTP/1.1", StringComparison.Ordinal);
                var parametersString = paramString.Substring(iqs, end - iqs);
                return HttpUtility.ParseQueryString(parametersString);
            }
            return new NameValueCollection();
        }

        public string GetRequestUri()
        {
            var reqMatch = Regex.Match(_requestString, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");

            var requestUri = "." + Uri.UnescapeDataString(reqMatch.Groups[1].Value);
            if (requestUri.EndsWith("/"))
            {
                requestUri += "index.html";
            }
            return requestUri;
        }
    }
}