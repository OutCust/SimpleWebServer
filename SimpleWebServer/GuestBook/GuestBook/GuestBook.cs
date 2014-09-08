using Server.Interfaces;

namespace GuestBook.GuestBook
{
    public class GuestBook : IPage
    {
        public string Process(IResponce responce, string text)
        {
            return text;
        }

        public string Path
        {
            get { return "./GuestBook/GuestBook.html"; }
        }
    }
}