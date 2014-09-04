using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IRequest
    {
        string RequestString { get; }
        
        IResponce GetErrorResponce(int errorCode);
        IResponce GetResponce();
    }
}