using System.Collections.Generic;
using MediatR;

namespace RegistrationForm.Infrastructure.DAL.Country
{
    public class GetCountriesListRequest: IRequest<IEnumerable<Models.Country>>
    {
    }
}
