using SoftJail.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
	[XmlType("Officer")]
    public class ImportOfficerPrisonerDto
    {
        [Required]
        [MinLength(3), MaxLength(30)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        [XmlElement("Money")]
        public decimal Money { get; set; }

        [Required]
        [EnumDataType(typeof (Position))]
        [XmlElement("Position")]
        public string Position { get; set; }

        [Required]
        [EnumDataType(typeof(Weapon))]
        [XmlElement("Weapon")]
        public string Weapon { get; set; }

        [Required]
        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }

        [Required]
        [XmlArray("Prisoners")]
        public ImportPrisonerDto[] Prisoners { get; set; }
    }

    //   <Officers>
    //<Officer>
    //	<Name>Minerva Kitchingman</Name>
    //	<Money>2582</Money>
    //	<Position>Invalid</Position>
    //	<Weapon>ChainRifle</Weapon>
    //	<DepartmentId>2</DepartmentId>
    //	<Prisoners>
    //		<Prisoner id = "15" />
    //	</ Prisoners >
    //</ Officer >
}
