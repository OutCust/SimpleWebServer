using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NLog;
using Server.Core.Components;
using Server.Interfaces;

namespace Server.Core
{
    public class Server : IDisposable
    {
        private readonly string _sitePath;
        private readonly Logger _logger = LogManager.GetLogger("Server");
        private readonly IRequestBuilder _requestBuilder = new RequestBuilder();

        private TcpListener _listner;

        public Server(string sitePath)
        {
            _sitePath = sitePath;
        }

        public void Start(int port)
        {
            _listner = new TcpListener(IPAddress.Any, port);
            _listner.Start();
            while (true)
            {
                ThreadPool.QueueUserWorkItem(stateInfo =>
                {
                    var uc = new UserClient(_requestBuilder);
                    uc.ProcessRequest(stateInfo as TcpClient, _sitePath);
                }, _listner.AcceptTcpClient());
            }
        }

        public void Stop()
        {
            _listner.Stop();
        }

        public void Dispose()
        {
            Stop();
            _listner = null;
        }
    }
}