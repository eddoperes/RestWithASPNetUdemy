using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Hypermedia.Filters;

namespace RestWithASPNetUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private ILoginBusiness _loginBusiness;

        public AuthController(ILogger<AuthController> logger, ILoginBusiness loginBusinnes)
        {
            _logger = logger;
            _loginBusiness = loginBusinnes;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult SignIn([FromBody] UserVO user)
        {
            if (user == null)
                return BadRequest("Invalid request");
            var token = _loginBusiness.ValidateCredentials(user); 
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null)
                return BadRequest("Invalid request");
            var token = _loginBusiness.ValidateCredentials(tokenVO);
            if (token == null)
                return BadRequest("Invalid request");
            return Ok(token);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var revoked = _loginBusiness.RevokeToken(userName);  
            if (!revoked)
                return BadRequest("Invalid request");
            return NoContent();
        }

    }
}