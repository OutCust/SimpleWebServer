using System;
using System.Net.Sockets;
using NLog;
using Server.Core.Components;
using Server.Interfaces;

namespace Server.Core
{
    public class UserClient : IUserClient
    {
        private readonly IRequestBuilder _builder;
        private readonly Logger _logger = LogManager.GetLogger("Server");
        public UserClient(IRequestBuilder builder)
        {
            _builder = builder;
        }

        public void ProcessRequest(TcpClient tcpClient)
        {
            using (var stream = tcpClient.GetStream())
            {
                IRequest request = _builder.BuildRequest(stream);
                _logger.Debug(request);

                IResponce responce = string.IsNullOrEmpty(request.RequestString) 
                    ? request.GetErrorResponce(500) 
                    : request.GetResponce();

                try
                {
                    responce.Process();
                }
                catch (Exception exc)
                {
                    _logger.Error("Что то пошло не так {0}", exc.Message);
                    responce = request.GetErrorResponce(500);
                }

                responce.Send();
            }
        }
    }
}