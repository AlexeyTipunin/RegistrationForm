using System;
using MediatR;
using RegistrationForm.Models;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class GetAccountByLoginIdRequest : IRequest<Account>
    {
        public GetAccountByLoginIdRequest(string login)
        {
            if(login == null)
                throw new ArgumentNullException(nameof(login));

            if(string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login))
                throw new ArgumentOutOfRangeException(nameof(login));

            Login = login;
        }

        public string Login { get; }
    }
}