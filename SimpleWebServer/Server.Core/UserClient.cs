using System;
using System.Net.Sockets;
using Server.Core.Components;
using Server.Interfaces;

namespace Server.Core
{
    public class UserClient : IUserClient
    {
        private readonly IRequestBuilder _builder;

        public UserClient(IRequestBuilder builder)
        {
            _builder = builder;
        }

        public void ProcessRequest(TcpClient tcpClient)
        {
            using (var stream = tcpClient.GetStream())
            {
                IRequest request = _builder.BuildRequest(stream);
                
                IResponce responce = string.IsNullOrEmpty(request.RequestString) 
                    ? request.GetErrorResponce(500) 
                    : request.GetResponce();

                try
                {
                    responce.Process();
                }
                catch (Exception exc)
                {
                    responce = request.GetErrorResponce(500);
                }

                responce.Send();
            }
        }
    }
}