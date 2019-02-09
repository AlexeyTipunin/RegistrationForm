namespace RegistrationForm.Models
{
    public class AccountWithPassword: Account
    {
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}