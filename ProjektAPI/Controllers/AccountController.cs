using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Model;
using ProjektAPI.Services;

namespace ProjektAPI.Controllers
{
    [Route("api/acount")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUser registerUser)
        {
            _accountService.RegisterUser(registerUser);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody]Login login)
        {
            string token = _accountService.GenerateJwt(login);
            return Ok(token);
        }
    }
}
