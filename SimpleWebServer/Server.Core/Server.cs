using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NLog;

namespace Server.Core
{
    public class Server : IDisposable
    {
        private readonly string _sitePath;
        private readonly Logger _logger = LogManager.GetLogger("Server");
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
                    var uc = new UserClient();
                    uc.ProcessRequest(stateInfo as TcpClient, _sitePath);
                }, _listner.AcceptTcpClient());
            }
        }

        public void Stop()
        {
            
        }

        public void Dispose()
        {
            Stop();
            _listner.Stop();
            _listner = null;
        }
    }
}