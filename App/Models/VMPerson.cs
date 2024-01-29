using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class VMPerson
    {
        [JsonProperty(PropertyName = "id")]
        public string? IDPerson { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        [Required]
        public string? FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        [Required]
        public string? LastName { get; set; } 

        [JsonProperty(PropertyName = "email")]
        [EmailAddress]
        public string? Email { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string? Country { get; set; } 

        [JsonProperty(PropertyName = "adress")]
        public string? Adress { get; set; } 

        [JsonProperty(PropertyName = "dateofbirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "contacts")]
        public ICollection<VMContact> Contact { get; set; } = new List<VMContact>();
    }
}
