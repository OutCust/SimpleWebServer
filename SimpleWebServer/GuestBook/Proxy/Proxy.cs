using System;
using System.Net;
using Server.Interfaces;

namespace GuestBook.Proxy
{
    public class Proxy :  IPage
    {
        public string ProcessRequest(IRequest request, string text)
        {
            var parameters = request.RequestData;

            var proxyUrl = parameters["url"];

            var webClient = new WebClient();

            var uriBuilder = new UriBuilder(proxyUrl);
            return webClient.DownloadString(uriBuilder.Uri);
        }

        public string Path { get { return "./Proxy/index.html"; } }
    }
}