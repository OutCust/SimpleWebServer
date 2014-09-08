using System;
using System.Collections.Generic;
using System.IO;
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

        public IResponce CreateResponce(string appPath, string filePath,  IRequest request)
        {
            IPage page = TryGetPage(appPath, filePath);

            if (page == null && !File.Exists(filePath))
            {
                return CreateErrorResponce(404, request);
            }
            if (page != null)
            {
                return new PageResponce(page, request, _contentTypeDefiner);
            }

            return new FileResponce(filePath, _contentTypeDefiner, request);
        }

        private IPage TryGetPage(string appPath, string filePath)
        {
            foreach (var registredPage in _registredPages)
            {
                var pagePath = Path.GetFullPath(Path.Combine(appPath, registredPage.Path));
                if (pagePath.Equals(filePath, StringComparison.InvariantCultureIgnoreCase))
                {
                    return registredPage;
                }
            }
            return null;
        }

        public IResponce CreateErrorResponce(int errorCode, IRequest request)
        {
            var responceBody = string.Format("<html><body><h1> {0} {1} </h1></body></html>", errorCode, ((HttpStatusCode)errorCode));
            var responceString = String.Format("HTTP/1.1 {0} {1} \nContent-type: text/html\nContent-Length: {2} \n\n {3}",
                                            errorCode,
                                            ((HttpStatusCode)errorCode),
                                            responceBody.Length,
                                            responceBody);
            var responce = new ResponceBase(request)
            {
                ResponceData = Encoding.UTF8.GetBytes(responceString)
            };
            return responce;
        }
    }
}