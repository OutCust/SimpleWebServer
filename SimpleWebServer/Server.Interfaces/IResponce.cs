using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IResponce
    {
        void Send(NetworkStream stream);

        void Process();
    }
}