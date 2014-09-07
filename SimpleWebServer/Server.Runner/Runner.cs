using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Runner
{
    class Runner
    {
        static void Main(string[] args)
        {
            using (var server = new Core.Server("./Site"))
            {
                server.Start(12345);
            }
        }
    }
}
