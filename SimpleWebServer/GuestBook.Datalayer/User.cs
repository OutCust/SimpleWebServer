using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace GuestBook.Datalayer
{
    [Serializable]
    public class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }

        
        [Key]
        [XmlIgnore]
        public long Id { get; set; }

        [XmlAttribute("userName")]
        public string Name { get; set; }
        public virtual HashSet<Message> Messages { get;  set; } 
    }
}