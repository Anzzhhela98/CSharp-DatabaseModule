using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.DataProcessor.ImportDto
{
    public class ImportEmployeeDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Username { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"([0-9]{3})-([0-9]{3})-([0-9]{4})$")]
        public string Phone { get; set; }

        public List<int> Tasks { get; set; }
    }

    //  {
    //"Username": "jstanett0",
    //"Email": "kknapper0@opera.com",
    //"Phone": "819-699-1096",
    //"Tasks": [
    //  34,
    //  32,
    //  65,
    //  30,
    //  30,
    //  45,
    //  36,
    //  67
    //]
//},
}
