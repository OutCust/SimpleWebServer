﻿using Server.Core;

namespace Server.Runner
{
    class Runner
    {
        static void Main(string[] args)
        {
            var settings = new ServerSettings
            {
                PortNumber = 12345,
                SitePath = "./Site"
            };

            using (var server = new Core.Server(settings))
            {
                server.Start();
            }
        }
    }
}
