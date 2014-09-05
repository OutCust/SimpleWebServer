namespace Server.Core.Components
{
    public interface IContentTypeDefiner
    {
        string GetContentTypeByExtension(string extension);
    }
}