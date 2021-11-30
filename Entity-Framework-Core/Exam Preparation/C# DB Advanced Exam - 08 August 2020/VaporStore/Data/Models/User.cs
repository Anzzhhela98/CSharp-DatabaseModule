using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Range(3, 103)]
        public string Age { get; set; }

        public ICollection<Card> Cards { get; set; } = new HashSet<Card>();
    }


    //•	Id – integer, Primary Key
    //•	Username – text with length[3, 20] (required)
    //•	FullName – text, which has two words, consisting of Latin letters. Both start with an upper letter and are followed by lower letters.The two words are separated by a single space (ex. "John Smith") (required)
    //•	Email – text(required)
    //•	Age – integer in the range [3, 103] (required)
    //•	Cards – collection of type Card

}
