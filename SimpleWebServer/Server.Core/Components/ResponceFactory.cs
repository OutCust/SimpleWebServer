using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Server.Core.Responces;
using Server.Interfaces;

namespace Server.Core.Components
{
    public class ResponceFactory : IResponceFactory
    {
        private readonly IContentTypeDefiner _contentTypeDefiner;
        private readonly IEnumerable<IPage> _registredPages;

        public ResponceFactory(IContentTypeDefiner contentTypeDefiner, IEnumerable<IPage> registredPages)
        {
            _contentTypeDefiner = contentTypeDefiner;
            _registredPages = registredPages;
        }

        public IResponce CreateResponce(string appPath, string filePath)
        {
            var isTextContent = !_contentTypeDefiner.GetContentTypeByExtension(Path.GetExtension(filePath)).Contains("text");
            if (isTextContent)
            {
                IPage page = TryGetPage(filePath);

                return page != null ? new PageResponce(page) : new ResponceBase();
            }
            return new FileResponce(filePath, _contentTypeDefiner);
        }

        private IPage TryGetPage(string filePath)
        {
            return _registredPages.FirstOrDefault(c => c.Path.Equals(filePath));
        }

        public IResponce CreateErrorResponce(int errorCode)
        {
            var responceBody = string.Format("<html><body><h1> {0} {1} </h1></body></html>", errorCode, ((HttpStatusCode)errorCode));
            var responceString = String.Format("HTTP/1.1 {0} {1} \nContent-type: text/html\nContent-Length: {2} \n\n {3}",
                                            errorCode,
                                            ((HttpStatusCode)errorCode),
                                            responceBody.Length,
                                            responceBody);
            var responce = new ResponceBase
            {
                ResponceData = Encoding.ASCII.GetBytes(responceString)
            };
            return responce;
        }
    }
}