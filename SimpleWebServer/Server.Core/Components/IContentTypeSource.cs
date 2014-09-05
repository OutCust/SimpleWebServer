namespace Server.Core.Components
{
    public interface IContentTypeSource
    {
        string GetContentTypeByExtension(string extension);
    }
}