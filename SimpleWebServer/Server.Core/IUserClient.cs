using System.Net.Sockets;

namespace Server.Core
{
    public interface IUserClient
    {
        void ProcessRequest(TcpClient tcpClient, string sitePath);
    }
}