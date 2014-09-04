using System.Collections.Specialized;

namespace Server.Core.Components
{
    public interface IRequestParser
    {
        string GetRequestType();
        NameValueCollection ExtractPostData();
        NameValueCollection ExtractGetData();
        string GetRequestUri();
    }
}