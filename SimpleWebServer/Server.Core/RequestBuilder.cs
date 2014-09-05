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
        private readonly IRequestDataSource _requestDataSource;

        public RequestBuilder(IRequestDataSource requestDataSource)
        {
            _requestDataSource = requestDataSource;
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

            var request = new Request(new ContentTypeSource())
            {
                RequestString = requestString,
                RequestType = requestType,
                RequestData = requestData,
                RequestUri = requestUri
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