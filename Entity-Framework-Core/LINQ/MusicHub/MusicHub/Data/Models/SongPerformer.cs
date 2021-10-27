using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MusicHub.Data.Models
{
    public class SongPerformer
    {
        [ForeignKey(nameof(Song))]
        [Required]
        public int SongId { get; set; }

        public Song Song { get; set; }

        [ForeignKey(nameof(Performer))]
        [Required]
        public int PerformerId { get; set; }

        [Required]
        public Performer Performer { get; set; }
    }
}