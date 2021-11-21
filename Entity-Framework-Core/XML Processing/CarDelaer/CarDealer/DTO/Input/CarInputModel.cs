using System.Xml.Serialization;
namespace CarDealer.DTO
{
    [XmlType("Car")]
    public class CarInputModel
    {
        [XmlElement("make")]
        public string  Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public PartsDto[] Parts { get; set; }
    }
}
