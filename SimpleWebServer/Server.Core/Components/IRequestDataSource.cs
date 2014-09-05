using System.Collections.Specialized;

namespace Server.Core.Components
{
    public interface IRequestDataSource
    {
        void SetRequestString(string requestString);
        string GetRequestType();
        NameValueCollection ExtractPostData();
        NameValueCollection ExtractGetData();
        string GetRequestUri();
    }
}