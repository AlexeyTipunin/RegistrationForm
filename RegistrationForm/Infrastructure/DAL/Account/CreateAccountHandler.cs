using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationForm.DAL.src.Context;
using RegistrationForm.DAL.src.Entities;
using RegistrationForm.Infrastructure.Security;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class CreateAccountHandler : AbsIRequestHandler<CreateAccountRequest, Models.Account>
    {
        protected readonly IPasswordEncrypter PasswordEncrypter;
        public CreateAccountHandler(RegistrationDbContext dbContext, IMapper mapper, IPasswordEncrypter passwordEncrypter) : base(dbContext, mapper)
        {
            PasswordEncrypter = passwordEncrypter ?? throw new ArgumentNullException(nameof(passwordEncrypter));
        }

        protected override async Task<Models.Account> InternalHandle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            if (await DbContext.Accounts.AnyAsync(x => x.Login == request.Account.Login, cancellationToken))
                throw new DuplicatedLoginDALException($"Account with login {request.Account.Login} already exist.");

            var account = Mapper.Map<Account>(request.Account);
            account.PasswordHash = PasswordEncrypter.Encrypt(request.Account.Password);
            DbContext.Accounts.Add(account);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<Models.Account>(account);
        }
    }
}