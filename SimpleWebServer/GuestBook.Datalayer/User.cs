using System;
using System.Xml.Serialization;

namespace GuestBook.Datalayer
{
    [Serializable]
    public class User
    {
        [XmlAttribute("userName")]
        public string Name { get; set; }
    }
}