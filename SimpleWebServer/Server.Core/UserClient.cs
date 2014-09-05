﻿using System.Net.Sockets;
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

        public void ProcessRequest(TcpClient tcpClient, string sitePath)
        {
            using (var stream = tcpClient.GetStream())
            {
                IRequest request = _builder.BuildRequest(stream);
                
                IResponce responce = string.IsNullOrEmpty(request.RequestString) 
                    ? request.GetErrorResponce(500) 
                    : request.GetResponce();

                responce.Process(request);
                
                responce.Send(stream);
            }
        }
    }
}