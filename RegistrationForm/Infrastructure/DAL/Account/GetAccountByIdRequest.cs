using MediatR;
using RegistrationForm.Models;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class GetAccountByIdRequest : IRequest<Account>
    {
        public GetAccountByIdRequest(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; }
    }
}