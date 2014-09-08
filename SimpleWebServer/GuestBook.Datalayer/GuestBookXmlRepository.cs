using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace GuestBook.Datalayer
{
    public class GuestBookXmlRepository : IGuestBookRepository
    {
        private readonly string _filePath;
        private readonly XmlSerializer _serializer;

        public GuestBookXmlRepository(string filePath)
        {
            _filePath = filePath;
            _serializer = new XmlSerializer(typeof (List<Message>), new []{typeof (User)});
        }

        public void AddMessage(Message message)
        {
            var messages = GetMessages();

            messages.Add(message);

            using (var xmlWriter = XmlWriter.Create(_filePath))
            {
                _serializer.Serialize(xmlWriter, messages);
                xmlWriter.Flush();
            }
        }

        public IList<Message> GetMessages()
        {
            var result = new List<Message>();
            if (!File.Exists(_filePath))
                return result;

            using (var reader = XmlReader.Create(_filePath))
            {
                var res = (IEnumerable<Message>)_serializer.Deserialize(reader);
                return res.OrderBy(c => c.Date).ToList();
            }
        }
    }
}