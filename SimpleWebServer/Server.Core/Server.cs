using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Ninject;
using NLog;
using Server.Core.Components;
using Server.Core.DI;
using Server.Interfaces;

namespace Server.Core
{
    public class Server : IDisposable
    {
        private readonly string _sitePath;
        private readonly Logger _logger = LogManager.GetLogger("Server");
        
        private readonly IKernel _kernel;
        private TcpListener _listner;

        public Server(string sitePath)
        {
            _sitePath = sitePath;
            _kernel = new StandardKernel();

            LoadModules(_kernel);
        }

        private void LoadModules(IKernel kernel)
        {
            kernel.Load<ServerModule>();
        }

        public void Start(int port)
        {
            _listner = new TcpListener(IPAddress.Any, port);
            _listner.Start();
            while (true)
            {
                ThreadPool.QueueUserWorkItem(stateInfo =>
                {
                    var uc = _kernel.Get<IUserClient>();
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