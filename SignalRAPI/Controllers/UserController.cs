using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRAPI.Core.Interfaces;
using SignalRAPI.Dtos;
using SignalRAPI.Dtos.UserDtos;
using System.Threading.Tasks;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserReadDto>> GetUserById(string id)
        {
            var result = await _userService.GetUserById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all-users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserReadDto>> GetAll([FromQuery] PagingDto paging)
        {
            var result = await _userService.GetAll(paging);
            return StatusCode(result.StatusCode, result);
        }
    }
}
