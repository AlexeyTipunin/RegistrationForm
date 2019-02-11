using System.ComponentModel.DataAnnotations;
using RegistrationForm.Infrastructure.Validators;

namespace RegistrationForm.Models
{
    public class Account
    {
        [Required]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Login is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Login must be a valid email.")]
        public string Login { get; set; }

        [BoolValidation(true, ErrorMessage = "You should agree to work for food.")]
        public bool Agreement { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Province is required.")]
        public int ProvinceId { get; set; }
    }
}