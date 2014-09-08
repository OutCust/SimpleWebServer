using System.Xml;
using System.Xml.Serialization;

namespace GuestBook
{
    public sealed class ConfigLoader
    {
        public T Load<T>(string filePath) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new XmlTextReader(filePath);
            var config = (T)serializer.Deserialize(reader);
            reader.Close();
            return config;
        }
    }
}