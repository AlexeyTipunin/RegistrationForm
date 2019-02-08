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
    public class GetCountriesListHandler: IRequestHandler<GetCountriesListRequest, IEnumerable<Models.Country>>
    {
        private readonly RegistrationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCountriesListHandler(RegistrationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        public async Task<IEnumerable<Models.Country>> Handle(GetCountriesListRequest request, CancellationToken cancellationToken)
        {
            var countries = await _dbContext.Countries.ToArrayAsync(cancellationToken);
            return countries.Select(x => _mapper.Map<Models.Country>(x));
        }
    }
}