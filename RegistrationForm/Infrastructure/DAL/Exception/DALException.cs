using System;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class DALException : Exception
    {
        public DALException(string message): base(message)
        {
            
        }

        public DALException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}