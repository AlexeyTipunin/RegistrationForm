using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationForm.DAL.src.Context;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class GetAccountByLoginHandler : AbsIRequestHandler<GetAccountByLoginIdRequest, Models.Account>
    {
        public GetAccountByLoginHandler(RegistrationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected override async Task<Models.Account> InternalHandle(GetAccountByLoginIdRequest request, CancellationToken cancellationToken)
        {
            var account = await DbContext.Accounts.FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken);
            return account != null
                ? Mapper.Map<Models.Account>(account)
                : null;
        }
    }
}