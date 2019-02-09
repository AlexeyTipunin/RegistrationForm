using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationForm.DAL.src.Context;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class GetAccountByIdHandler : AbsIRequestHandler<GetAccountByIdRequest, Models.Account>
    {
        public GetAccountByIdHandler(RegistrationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected override async Task<Models.Account> InternalHandle(GetAccountByIdRequest request, CancellationToken cancellationToken)
        {
            var account = await DbContext.Accounts.FirstOrDefaultAsync(x => x.AccountId == request.AccountId, cancellationToken);
            return account != null 
                ? Mapper.Map<Models.Account>(account) 
                : null;
        }
    }
}