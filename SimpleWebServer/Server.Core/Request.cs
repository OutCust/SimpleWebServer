using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Sockets;
using Server.Core.Components;
using Server.Interfaces;

namespace Server.Core
{
    public class Request : IRequest
    {
        private readonly IResponceFactory _responceFactory;

        public Request(IResponceFactory responceFactory, NetworkStream stream)
        {
            _responceFactory = responceFactory;
            RequestStream = stream;
        }

        public string RequestString { get; set; }

        public string RequestType { get; set; }

        public NameValueCollection RequestData { get; set; }

        public string RequestUri { get; set; }

        public string SitePath { get; set; }
        public NetworkStream RequestStream { get; private set; }

        public IResponce GetErrorResponce(int errorCode)
        {
            return _responceFactory.CreateErrorResponce(errorCode, this);
        }
        public IResponce GetResponce()
        {
            var appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SitePath);
            var filePath = Path.GetFullPath(Path.Combine(appPath, RequestUri));

            return _responceFactory.CreateResponce(appPath, filePath, this);
        }

    }
}