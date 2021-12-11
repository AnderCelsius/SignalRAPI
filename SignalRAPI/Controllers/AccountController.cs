using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRAPI.Core.Interfaces;
using SignalRAPI.Dtos.AuthenticationDtos;
using SignalRAPI.Utilities;
using System.Threading.Tasks;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<LoginResponse>>> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.Login(request, GenerateIPAddress());
            return StatusCode(result.StatusCode, result);
        }      

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
