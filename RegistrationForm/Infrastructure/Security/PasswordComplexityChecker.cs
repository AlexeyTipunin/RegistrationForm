using System.Linq;

namespace RegistrationForm.Infrastructure.Security
{
    public class PasswordComplexityChecker : IPasswordComplexityChecker
    {
        public bool Check(string password)
        {
            return password.Any(char.IsDigit) && password.Any(char.IsLetter);
        }
    }
}