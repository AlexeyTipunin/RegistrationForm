using System.Collections.Generic;
using MediatR;
using RegistrationForm.Models;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class GetAccountsListRequest : IRequest<IEnumerable<Account>>
    {

    }
}