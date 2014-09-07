using System;
using System.IO;

namespace Server.Interfaces
{
    public class HelloWorld : IPage
    {
        public string ProcessRequest(IRequest request, string text)
        {
            return String.Format(text, "Hello World");
        }

        public string Path { get { return "./Pages/HelloWorld.html"; } }
    }
}
