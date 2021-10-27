using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MusicHub.Data.Models
{
    public class Producer
    {
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [MaxLength(40)]
        public string Pseudonym { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    }
}