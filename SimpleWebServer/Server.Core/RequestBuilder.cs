using System;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Text;
using Server.Core.Components;
using Server.Interfaces;

namespace Server.Core
{
    public class RequestBuilder : IRequestBuilder
    {
        private readonly ServerSettings _settings;
        private readonly IRequestDataSource _requestDataSource;
        private readonly IResponceFactory _responceFactory;

        public RequestBuilder(ServerSettings settings, IRequestDataSource requestDataSource, IResponceFactory responceFactory)
        {
            _settings = settings;
            _requestDataSource = requestDataSource;
            _responceFactory = responceFactory;
        }

        private const int BufferLength = 2048;

        public IRequest BuildRequest(NetworkStream stream)
        {

            var requestString = GetRequestString(stream);
            _requestDataSource.SetRequestString(requestString);
            var requestType = _requestDataSource.GetRequestType();

            NameValueCollection requestData;
            switch (requestType)
            {
                case "POST":
                    requestData = _requestDataSource.ExtractPostData();
                    break;
                case "GET":
                    requestData = _requestDataSource.ExtractGetData();
                    break;
                default:
                    throw new Exception("can't get request type");
            }

            var requestUri = _requestDataSource.GetRequestUri();

            var request = new Request(_responceFactory, stream)
            {
                RequestString = requestString,
                RequestType = requestType,
                SitePath = _settings.SitePath,
                RequestData = requestData,
                RequestUri = requestUri,
                RequestStream = stream,
            };
            return request;
        }


        private string GetRequestString(NetworkStream stream)
        {
            var result = new StringBuilder();
            var buffer = new byte[BufferLength];

            if (!stream.CanRead)
                return String.Empty;

            do
            {
                stream.Read(buffer, 0, BufferLength);
                result.AppendFormat("{0}", Encoding.ASCII.GetString(buffer));
            } while (stream.DataAvailable);
            return result.ToString().Trim('\0');
        }
    }
}