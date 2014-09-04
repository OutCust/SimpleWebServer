using System.Net.Sockets;
using Server.Interfaces;

namespace Server.Core.Components
{
    public interface IRequestBuilder
    {
        IRequest BuildRequest(NetworkStream stream);
    }
}