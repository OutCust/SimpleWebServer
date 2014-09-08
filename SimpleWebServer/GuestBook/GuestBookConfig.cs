using System.Xml.Serialization;

namespace GuestBook
{
    [XmlRoot("GuestBook")]
    public class GuestBookConfig
    {
        [XmlAttribute("RepositoryPath")]
        public string RepositoryPath { get; set; }

        [XmlAttribute("RepositoryType")]
        public string RepositoryType { get; set; }
    }
}