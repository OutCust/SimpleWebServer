using System;
using System.Collections.Specialized;
using System.IO;
using Server.Core.Components;
using Server.Interfaces;

namespace Server.Core
{
    public class Request : IRequest
    {
        private readonly IResponceFactory _responceFactory;

        public Request(IResponceFactory responceFactory)
        {
            _responceFactory = responceFactory;
        }

        public string RequestString { get; set; }

        public string RequestType { get; set; }

        public NameValueCollection RequestData { get; set; }

        public string RequestUri { get; set; }

        public string SitePath { get; set; }

        public IResponce GetErrorResponce(int errorCode)
        {
            return _responceFactory.CreateErrorResponce(errorCode);
        }

        public IResponce GetResponce()
        {
            var appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SitePath);
            var filePath = Path.GetFullPath(Path.Combine(appPath, RequestUri));

            return _responceFactory.CreateResponce(appPath, filePath);
        }


    }
}