namespace Server.Interfaces
{
    public abstract class BasePage : IPage
    {
        public virtual string ProcessRequest(IRequest request, string text)
        {
            return text;
        }

        public string Path 
        {
            get { return "/"; }
        }
    }
}