using System;
using System.Text;
using GuestBook.Datalayer;
using Server.Interfaces;

namespace GuestBook.GuestBook
{
    public class GuestBook : IPage
    {
        private readonly IGuestBookRepository _repository;

        public GuestBook()
        {
            var locator = NinjectServiceLocator.GetInstance();
            _repository = locator.Get<IGuestBookRepository>();
        }

        public string Process(IResponce responce, string text)
        {
            var request = responce.Request;
            if (request.RequestType.Equals("POST"))
            {
                var parameters = request.RequestData;
                var userName = parameters["user"];
                var messageText = parameters["message"];

                var user = new User() { Name = userName };
                var message = new Message() { Date = DateTime.Now, Text = messageText, User = user };
                _repository.AddMessage(message);
            }

            var messages = _repository.GetMessages();
            var sb = new StringBuilder();

            foreach (var message in messages)
            {
                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", message.User.Name, message.Text);
            }

            return string.Format(text, sb);
        }

        public string Path
        {
            get { return "./GuestBook/GuestBook.html"; }
        }
    }
}