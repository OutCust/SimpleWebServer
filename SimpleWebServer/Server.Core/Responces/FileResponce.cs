using System;
using System.IO;
using System.Text;
using Server.Core.Components;
using Server.Interfaces;

namespace Server.Core.Responces
{
    public class FileResponce : ResponceBase
    {
        private readonly string _filePath;
        private readonly IContentTypeDefiner _contentTypeDefiner;

        public FileResponce(string filePath, IContentTypeDefiner contentTypeDefiner, IRequest request)
            : base(request)
        {
            _filePath = filePath;
            _contentTypeDefiner = contentTypeDefiner;
        }

        public override void Process()
        {
            var extension = Path.GetExtension(_filePath);
                
            using (var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var headers = string.Format("HTTP/1.1 200 OK\nContent-Type: {0}\nContent-Length: {1}\n\n",
                                    _contentTypeDefiner.GetContentTypeByExtension(extension),
                                    fs.Length);
                byte[] headersBytes = Encoding.ASCII.GetBytes(headers);

                using (var ms = new MemoryStream())
                {
                    ms.Write(headersBytes, 0, headersBytes.Length);
                    fs.CopyTo(ms);
                    ResponceData = ms.ToArray();
                }
            }
        }
    }
}