using System.Security.Cryptography.X509Certificates;

namespace Server.Interfaces
{
    public interface IPage
    {
        string Process(IResponce responce, string text);

        string Path { get; }
    }
}