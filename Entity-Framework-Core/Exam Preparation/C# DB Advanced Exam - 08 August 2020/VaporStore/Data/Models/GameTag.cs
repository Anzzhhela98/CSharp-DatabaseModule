using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
   public class GameTag
    {
        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }

        [Required]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
//•	GameId – integer, Primary Key, foreign key(required)
//•	Game – Game
//•	TagId – integer, Primary Key, foreign key(required)
//•	Tag – Tag
}
