using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IResponce
    {
        byte[] ResponceData { get; set; }
        IRequest Request { get; }
        void Send();
        void Process();
        void Redirect(string url);
    }
}