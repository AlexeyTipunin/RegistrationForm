using System;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class DuplicatedLoginDALException : DALException
    {
        public DuplicatedLoginDALException(string message) : base(message)
        {
        }

        public DuplicatedLoginDALException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}