namespace Server.Interfaces
{
    public class BasePage : IPage
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