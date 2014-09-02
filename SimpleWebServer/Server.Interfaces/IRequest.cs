using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IRequest
    {
        void SendResponce(NetworkStream stream);
    }
}