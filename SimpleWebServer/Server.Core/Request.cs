using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using Server.Core.Responces;
using Server.Interfaces;

namespace Server.Core
{
    public class Request : IRequest
    {
        public string RequestString { get; set; }

        public string RequestType { get; set; }

        public NameValueCollection RequestData { get; set; }

        public string RequestUri { get; set; }

        public string SitePath { get; set; }

        public IResponce GetErrorResponce(int errorCode)
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

        public IResponce GetResponce()
        {
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(appPath, SitePath, RequestUri);

            var isFilePath = !GetContentTypeByExtension(Path.GetExtension(filePath)).Contains("text");
            if (isFilePath)
            {
                return new FileResponce(filePath);
            }
            return new PageResponce(null);
        }

        public static string GetContentTypeByExtension(string extension)
        {
            var dict = new Dictionary<string, string>
            {
                {".htm", "text/html"},
                {".html", "text/html"},
                {".css", "text/stylesheet"},
                {".js", "text/javascript"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".png", "image/png"},
                {".ico", "image/ico"},
                {".gif", "image/gif"}
            };
            string result;
            dict.TryGetValue(extension, out result);
            return string.IsNullOrEmpty(result) ? "application/unknown" : result;
        }

    }
}