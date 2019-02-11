using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using RegistrationForm.Infrastructure.Validators;

namespace RegistrationForm.Models
{
    public class AccountWithPassword: Account
    {
        [Required(ErrorMessage ="Password is required.")]
        [SecurePassword(ErrorMessage = "Password must contains min 1 digit and min 1 letter.")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare(nameof(Password), ErrorMessage = "Password confirmation must be the same with password.")]
        public string PasswordConfirmation { get; set; }
    }
}