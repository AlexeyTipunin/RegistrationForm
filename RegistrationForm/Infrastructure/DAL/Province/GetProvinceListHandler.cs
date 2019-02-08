using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RegistrationForm.DAL.src.Context;

namespace RegistrationForm.Infrastructure.DAL.Province
{
    public class GetProvinceListHandler : IRequestHandler<GetProvinceListRequest, IEnumerable<Models.Province>>
    {
        private readonly RegistrationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetProvinceListHandler(RegistrationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        public async Task<IEnumerable<Models.Province>> Handle(GetProvinceListRequest request, CancellationToken cancellationToken)
        {
            var provinces = await _dbContext.Provinces.Where(x => x.CountryId == request.CountryId)
                .ToArrayAsync(cancellationToken);
            return provinces.Select(x => _mapper.Map<Models.Province>(x));
        }
    }
}