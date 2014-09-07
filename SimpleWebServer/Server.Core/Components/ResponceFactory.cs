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
            if (!File.Exists(filePath))
            {
                return CreateErrorResponce(404);
            }

            var isTextContent = _contentTypeDefiner.GetContentTypeByExtension(Path.GetExtension(filePath)).Contains("text");
            if (isTextContent)
            {
                IPage page = TryGetPage(appPath, filePath);

                if (page != null)
                {
                    return new PageResponce(page, _contentTypeDefiner);
                }
                var data = File.ReadAllBytes(filePath);
                return new ResponceBase
                {
                    ResponceData = data
                };
            }
            return new FileResponce(filePath, _contentTypeDefiner);
        }

        private IPage TryGetPage(string appPath, string filePath)
        {

            foreach (var registredPage in _registredPages)
            {
                var pagePath = Path.GetFullPath(Path.Combine(appPath, registredPage.Path));
                if (pagePath.Equals(filePath))
                {
                    return registredPage;
                }
            }
            return null;
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