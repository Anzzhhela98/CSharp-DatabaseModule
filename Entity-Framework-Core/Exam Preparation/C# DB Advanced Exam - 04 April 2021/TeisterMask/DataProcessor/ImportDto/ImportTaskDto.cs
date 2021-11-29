using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class ImportTaskDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [Required]
        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [Range(0, 3)]
        [XmlElement("ExecutionType")]
        public int ExecutionType { get; set; }


        [Range(0, 4)]
        [XmlElement("LabelType")]
        public int LabelType { get; set; }
    }
}

//< Name > Australian </ Name >
//      < OpenDate > 19 / 08 / 2018 </ OpenDate >
//      < DueDate > 13 / 07 / 2019 </ DueDate >
//      < ExecutionType > 2 </ ExecutionType >
//      < LabelType > 0 </ LabelType >
