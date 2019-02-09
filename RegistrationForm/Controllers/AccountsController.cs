using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegistrationForm.Infrastructure.DAL.Country;
using RegistrationForm.Infrastructure.Security;
using RegistrationForm.Models;

namespace RegistrationForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        protected readonly IMediator Mediator;
        protected readonly IPasswordComplexityChecker ComplexityChecker;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IMediator mediator, IPasswordComplexityChecker complexityChecker, ILogger<AccountsController> logger)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            ComplexityChecker = complexityChecker ?? throw new ArgumentNullException(nameof(complexityChecker));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var actions = await Mediator.Send(new GetAccountsListRequest());
            return Ok(actions);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var account = await Mediator.Send(new GetAccountByIdRequest(id));
            return account == null
                ? NotFound() as IActionResult
                : Ok(account);
        }

        // GET: /api/Accounts/GetByLogin/login
        [Route("GetByLogin/{login}")]
        [HttpGet]
        public async Task<IActionResult> GetByLogin(string login)
        {
            var account = await Mediator.Send(new GetAccountByLoginIdRequest(login));
            return account == null
                ? NotFound() as IActionResult
                : Ok(account);
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountWithPassword account)
        {

            if(!account.AgreeToWorkForFood)
                return BadRequest("I should agree to work for food.");

            if (!string.Equals(account.Password, account.PasswordConfirmation))
                return BadRequest("Account password confirmation does not miss mitch.");

            if (!ComplexityChecker.Check(account.Password))
                return BadRequest("Account password is weak.");

            try
            {
                var createdAccount = await Mediator.Send(new CreateAccountRequest(account));
                return CreatedAtAction(nameof(Get), new {id = createdAccount.AccountId }, createdAccount);
            }
            catch (DuplicatedLoginDALException e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(e.Message);
            }
            catch (DALException e)
            {
                _logger.LogError("Attempt to add account failed", e);
                return BadRequest("Attempt to add account failed.");
            }
        }
    }
}
