using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Interfaces;

namespace GuestBook.Pages
{
    public class IndexPage : IPage
    {
        public string ProcessRequest(IRequest request, string text)
        {
            return text;
        }

        public string Path { get; private set; }
    }
}
