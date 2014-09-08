using System;
using System.Configuration;
using NLog;
using Server.Core;

namespace Server.Runner
{
    class Runner
    {
        private static readonly Logger Logger = LogManager.GetLogger("Server");
        static void Main(string[] args)
        {
            var settings = (ServerSettings)ConfigurationManager.GetSection("serverSettings");

            using (var server = new Core.Server(settings))
            {
                Console.WriteLine("qweqwe");
                Logger.Info("Start Server");
                server.Start();
            }
        }
    }
}
