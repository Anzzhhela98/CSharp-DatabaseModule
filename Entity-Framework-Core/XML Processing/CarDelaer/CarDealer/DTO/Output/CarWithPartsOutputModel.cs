using System.Xml.Serialization;
namespace CarDealer.DTO.Output
{
    [XmlType("car")]
   public class CarWithPartsOutputModel
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }


        [XmlArray("parts")]
        public PartDto[] Parts { get; set; }
    }

    [XmlType("part")]
    public class PartDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
