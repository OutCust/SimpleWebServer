using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace GuestBook.Datalayer
{
    [Serializable]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [XmlAttribute("userName")]
        public string Name { get; set; }
    }
}