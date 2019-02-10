using Microsoft.EntityFrameworkCore.Internal;

namespace RegistrationForm.Infrastructure.Security
{
    public interface IPasswordEncrypter
    {
        string Encrypt(string password);
    }
}