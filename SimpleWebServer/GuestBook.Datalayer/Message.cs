using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace GuestBook.Datalayer
{
    [Serializable]
    [XmlRoot("Message")]
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [XmlElement("user")]
        public User User { get; set; }

        [XmlElement("text")]
        public string Text { get; set; }

        [XmlAttribute("date")]
        public DateTime Date { get; set; }
    }
}