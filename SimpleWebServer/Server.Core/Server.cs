using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Ninject;
using NLog;
using Server.Core.DI;
using Ninject.Extensions.Conventions;

using Server.Interfaces;

namespace Server.Core
{
    public class Server : IDisposable
    {
        private readonly ServerSettings _settings;
        private readonly Logger _logger = LogManager.GetLogger("Server");
        
        private readonly IKernel _kernel;
        private TcpListener _listner;

        public Server(ServerSettings settings)
        {
            _settings = settings;
            _kernel = new StandardKernel();

            LoadModules(_kernel);

            _kernel.Bind<ServerSettings>().ToConstant(settings);
        }

        private void LoadModules(IKernel kernel)
        {
            kernel.Load<ServerModule>();

            kernel.Bind(x => x.FromAssembliesInPath(_settings.SitePath)
                .SelectAllClasses().InheritedFrom<IPage>()
                .BindAllInterfaces());

            kernel.Bind(x => x.FromAssembliesInPath(_settings.SitePath)
                .SelectAllClasses().InheritedFrom<ISiteInitializer>()
                .BindAllInterfaces());
        }

        public void Start()
        {
            _listner = new TcpListener(IPAddress.Any, _settings.PortNumber);

            _kernel.GetAll<ISiteInitializer>().First().Initialize(_settings.SiteConfigPath);

            _listner.Start();
            while (true)
            {
                ThreadPool.QueueUserWorkItem(stateInfo =>
                {
                    var uc = _kernel.Get<IUserClient>();
                    _logger.Debug("Start process request from User");
                    uc.ProcessRequest(stateInfo as TcpClient);
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