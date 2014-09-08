using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace GuestBook.Datalayer
{
    [Serializable]
    [XmlRoot("Message")]
    public class Message
    {
        [Key]
        [XmlIgnore]
        public long Id { get; set; }

        [XmlElement("user")]
        public virtual User User { get; set; }
        
        [Column("User_Id")]
        [XmlIgnore]
        public long? UserId { get; set; }

        [XmlElement("text")]
        public string Text { get; set; }

        [XmlAttribute("date")]
        public DateTime Date { get; set; }
    }
}