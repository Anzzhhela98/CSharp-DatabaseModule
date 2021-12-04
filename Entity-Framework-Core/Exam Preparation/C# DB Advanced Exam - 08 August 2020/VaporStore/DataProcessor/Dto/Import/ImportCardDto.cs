using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportCardDto
    {
        [Required]
        [RegularExpression(@"^(\d){4} (\d){4} (\d){4} (\d){4}$")]
        public string Number { get; set; }

        [Required]
        [JsonProperty("CVC")]
        [RegularExpression(@"^\d{3}$")]
        public string Cvc { get; set; }

        [Required]
        [Range(0,1)]
        public CardType Type { get; set; }
    }

}
