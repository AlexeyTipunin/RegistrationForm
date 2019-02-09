using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationForm.DAL.src.Context;
using RegistrationForm.DAL.src.Entities;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class GetAccountsListHandler : AbsIRequestHandler<GetAccountsListRequest, IEnumerable<Models.Account>>
    {
        public GetAccountsListHandler(RegistrationDbContext dbContext, IMapper mapper): base(dbContext, mapper)
        {
        }

        protected override async Task<IEnumerable<Models.Account>> InternalHandle(GetAccountsListRequest request, CancellationToken cancellationToken)
        {
            var accounts = await DbContext.Accounts.ToArrayAsync(cancellationToken);
            return accounts.Select(x => Mapper.Map<Models.Account>(x));
        }
    }
}