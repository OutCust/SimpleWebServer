using System.Collections.Specialized;
using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IRequest
    {
        string RequestString { get; set; }
        string RequestType { get; set; }
        NameValueCollection RequestData { get; set; }
        string RequestUri { get; set; }
        string SitePath { get; set; }
        NetworkStream RequestStream { get; }
        IResponce GetErrorResponce(int errorCode);
        IResponce GetResponce();
    }
}