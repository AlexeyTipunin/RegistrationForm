using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RegistrationForm.DAL.src.Context;
using RegistrationForm.Infrastructure.DAL.Country;

namespace RegistrationForm.Infrastructure.DAL.Province
{
    public class GetProvinceListHandler : AbsIRequestHandler<GetProvinceListRequest, IEnumerable<Models.Province>>
    {
        public GetProvinceListHandler(RegistrationDbContext dbContext, IMapper mapper): base(dbContext, mapper)
        {
        }

        protected override async Task<IEnumerable<Models.Province>> InternalHandle(GetProvinceListRequest request, CancellationToken cancellationToken)
        {
            var provinces = await DbContext.Provinces.Where(x => x.CountryId == request.CountryId)
                .ToArrayAsync(cancellationToken);
            return provinces.Select(x => Mapper.Map<Models.Province>(x));
        }
    }
}