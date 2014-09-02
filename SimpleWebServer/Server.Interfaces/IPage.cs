using System.Security.Cryptography.X509Certificates;

namespace Server.Interfaces
{
    public interface IPage
    {
        string ProcessRequest(IRequest request, string text);

        string Path { get; }
    }
}