using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<GameTag> GameTags { get; set; } = new HashSet<GameTag>();
    }
//•	Id – integer, Primary Key
//•	Name – text(required)
//•	GameTags - collection of type GameTag
}
