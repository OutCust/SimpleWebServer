using System;
using Server.Interfaces;

namespace GuestBook.Pages
{
    public class HelloWorld : IPage
    {
        public string Process(IResponce responce, string text)
        {
            return String.Format(text, "Hello World");
        }

        public string Path { get { return "./Pages/HelloWorld.html"; } }
    }
}
