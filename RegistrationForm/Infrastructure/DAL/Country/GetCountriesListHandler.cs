using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RegistrationForm.DAL.src.Context;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class GetCountriesListHandler: AbsIRequestHandler<GetCountriesListRequest, IEnumerable<Models.Country>>
    {
        public GetCountriesListHandler(RegistrationDbContext dbContext, IMapper mapper): base(dbContext, mapper)
        {
        }

        protected override async Task<IEnumerable<Models.Country>> InternalHandle(GetCountriesListRequest request, CancellationToken cancellationToken)
        {
            var countries = await DbContext.Countries.ToArrayAsync(cancellationToken);
            return countries.Select(x => Mapper.Map<Models.Country>(x));
        }
    }
}