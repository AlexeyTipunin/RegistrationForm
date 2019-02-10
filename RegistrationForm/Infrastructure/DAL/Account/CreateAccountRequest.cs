using System;
using MediatR;
using RegistrationForm.Models;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class CreateAccountRequest : IRequest<Account>
    {
        public CreateAccountRequest(AccountWithPassword account)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
        }

        public AccountWithPassword Account { get; }
    }
}