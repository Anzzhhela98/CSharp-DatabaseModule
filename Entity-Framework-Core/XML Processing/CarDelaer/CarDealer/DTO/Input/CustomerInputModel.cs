﻿using System;
using System.Xml.Serialization;
namespace CarDealer.DTO
{
    [XmlType("Customer")]
    public class CustomerInputModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("birthDate")]
        public DateTime BirthDate { get; set; }

        [XmlElement("isYoungDriver")]
        public bool IsYoungDriver { get; set; }
    }
}
