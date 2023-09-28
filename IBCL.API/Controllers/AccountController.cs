using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Response.Account;
using IBCL.Application.Common.Models.Response.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBCL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRegistrationModel userModel)
        {

          return Ok(await _accountService.RegisterAsync(userModel));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AccountTokenDto>> LoginAsync(AccountLoginModel request)
        {
            return Ok(await _accountService.LoginAsync(request));
        }
    }
}
