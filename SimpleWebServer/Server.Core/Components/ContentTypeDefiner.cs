using System.Collections.Generic;

namespace Server.Core.Components
{
    public class ContentTypeDefiner : IContentTypeDefiner
    {
        public ContentTypeDefiner()
        {
            _contentTypes = new Dictionary<string, string>
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
        }

        private readonly Dictionary<string, string> _contentTypes;

        
        public string GetContentTypeByExtension(string extension)
        {
            string result;
            _contentTypes.TryGetValue(extension, out result);
            return string.IsNullOrEmpty(result) ? "application/unknown" : result;
        }
 
    }
}