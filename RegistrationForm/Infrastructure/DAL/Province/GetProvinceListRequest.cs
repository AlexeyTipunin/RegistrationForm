using System.Collections.Generic;
using MediatR;

namespace RegistrationForm.Infrastructure.DAL.Province
{
    public class GetProvinceListRequest : IRequest<IEnumerable<Models.Province>>
    {
        public GetProvinceListRequest(int countryId)
        {
            CountryId = countryId;
        }

        public int CountryId { get; }
    }
}