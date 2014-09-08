using System.Collections.Generic;

namespace GuestBook.Datalayer
{
    public interface IGuestBookRepository
    {
        void AddMessage(Message message);
        IList<Message> GetMessages();
    }
}