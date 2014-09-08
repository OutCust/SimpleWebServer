using System.IO;
using System.Net.Sockets;
using System.Text;
using Server.Core.Components;
using Server.Interfaces;

namespace Server.Core.Responces
{
    public class PageResponce: ResponceBase
    {
        private readonly IPage _page;
        private readonly IContentTypeDefiner _contentTypeDefiner;

        public PageResponce(IPage page, IRequest request, IContentTypeDefiner contentTypeDefiner)
            : base(request)
        {
            _page = page;
            _contentTypeDefiner = contentTypeDefiner;
        }

        public override void Process()
        {
            var path = Path.GetFullPath(Path.Combine(Request.SitePath, _page.Path));
            var file = File.ReadAllText(path);

            string processedPage = _page.Process(this, file);
            if (IsRedirect)
            {
                return;
            }
            
            var headers = string.Format("HTTP/1.1 200 OK\nContent-Type: {0}\nContent-Length: {1}\n\n",
                            _contentTypeDefiner.GetContentTypeByExtension(Path.GetExtension(path)),
                            processedPage.Length);

            byte[] headersBytes = Encoding.ASCII.GetBytes(headers);
            byte[] pageBytes = Encoding.ASCII.GetBytes(processedPage); 
            using (var ms = new MemoryStream())
            {
                ms.Write(headersBytes, 0, headersBytes.Length);
                ms.Write(pageBytes, 0, pageBytes.Length);
                ResponceData = ms.ToArray();
            }
        }
    }
}