using System.Xml.Serialization;
namespace CarDealer.DTO
{
    [XmlType("partId")]
    public class PartsDto
    {
        [XmlAttribute("id")]
        public int PartId { get; set; }
    }
}
