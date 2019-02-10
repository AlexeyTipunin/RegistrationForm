using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RegistrationForm.DAL.src.Context;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public abstract class AbsIRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        protected readonly RegistrationDbContext DbContext;
        protected readonly IMapper Mapper;

        protected AbsIRequestHandler(RegistrationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await InternalHandle(request, cancellationToken);
            }
            catch (DALException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DALException($"Unexpected exception occured in {GetType().FullName}", e);
            }
        }

        protected abstract Task<TResponse> InternalHandle(TRequest request, CancellationToken cancellationToken);
    }
}