using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Color
    {
        public Color()
        {
            this.PrimaryKitTeams = new HashSet<Team>();
            this.SecondaryKitTeams = new HashSet<Team>();
        }
        public int ColorId { get; set; }

        public string Name { get; set; }
        [NotMapped]
        public virtual ICollection<Team> PrimaryKitTeams { get; set; }
        [NotMapped]
        public virtual ICollection<Team> SecondaryKitTeams { get; set; }
    }
}
