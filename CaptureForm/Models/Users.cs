using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CaptureForm.Models
{
    [Serializable]
    [XmlRoot("Users"), XmlType("Users")]
    public class Users
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Surname")]
        public string Surname { get; set; }

        [XmlElement("CellNo")]
        public string CellNo { get; set; }
    }
}