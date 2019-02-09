namespace RegistrationForm.Infrastructure.Security
{
    public interface IPasswordComplexityChecker
    {
        bool Check(string password);
    }
}