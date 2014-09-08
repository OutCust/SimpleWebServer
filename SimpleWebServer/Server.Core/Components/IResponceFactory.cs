using System.Net.Sockets;
using Server.Interfaces;

namespace Server.Core.Components
{
    public interface IResponceFactory
    {
        IResponce CreateResponce(string appPath, string filePath, IRequest request);
        IResponce CreateErrorResponce(int errorCode, IRequest request);
    }
}