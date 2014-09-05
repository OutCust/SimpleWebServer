using Server.Interfaces;

namespace Server.Core.Components
{
    public interface IResponceFactory
    {
        IResponce CreateResponce(string appPath, string filePath);
        IResponce CreateErrorResponce(int errorCode);
    }
}