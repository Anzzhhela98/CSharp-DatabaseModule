using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
  public  class ImportDepartmentCellDto
    {
        [Required]
        [MaxLength(25), MinLength(3)]
        public string Name { get; set; }
        public IEnumerable<ImportCellDto> Cells { get; set; }
    }

    public class ImportCellDto
    {
        [Required]
        [Range(1, 1000)]
        public int CellNumber { get; set; }

        [Required]
        public bool HasWindow { get; set; }
    }
}
