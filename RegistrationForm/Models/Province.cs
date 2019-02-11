using System.ComponentModel.DataAnnotations;

namespace RegistrationForm.Models
{
    public class Province
    {
        [Required]
        public int ProvinceId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}