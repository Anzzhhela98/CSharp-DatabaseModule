using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }

        public DateTime? DueDate { get; set; }

        public ICollection<Task> Tasks { get; set; } = new HashSet<Task>();

    }
}
//    •	Id - integer, Primary Key
//•	Name - text with length[2, 40] (required)
//•	OpenDate - date and time(required)
//•	DueDate - date and time(can be null)
//•	Tasks - collection of type Task
