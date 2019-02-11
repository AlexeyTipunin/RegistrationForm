using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegistrationForm.Infrastructure.DAL.Province;
using RegistrationForm.Models;

namespace RegistrationForm.Controllers
{
    [Route("api/Countries/{countryId}/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvincesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/Provinces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Province>>> Get(int countryId)
        {
            return Ok(await _mediator.Send(new GetProvinceListRequest(countryId)));
        }
    }
}
