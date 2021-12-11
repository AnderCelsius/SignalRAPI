using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRAPI.Core.Interfaces;
using SignalRAPI.Dtos;
using SignalRAPI.Dtos.RequestFormDtos;
using SignalRAPI.Utilities;
using SignalRAPI.Utilities.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestFormController : ControllerBase
    {
        private readonly IRequestFormService _requestFormService;

        public RequestFormController(IRequestFormService requestFormService)
        {
            _requestFormService = requestFormService;
        }

        [HttpGet("all-request-forms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>>> GetAll([FromQuery]PagingDto paging)
        {
            var result = await _requestFormService.GetAll(paging);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{formId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RequestFormReadDto>> GetByFormId(int formId)
        {
            var result = await _requestFormService.Get(formId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RequestFormReadDto>> GetByRequesterId([FromQuery]PagingDto paging, string userId)
        {
            var result = await _requestFormService.GetRequestorRequestForms(paging, userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{formStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RequestFormReadDto>> GetByFormStatus([FromQuery] PagingDto paging, string formStatus)
        {
            var result = await _requestFormService.GetFormByStatus(paging, formStatus);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("create-new-request")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RequestFormReadDto>> CreateNewRequest([FromBody] RequestFormCreateDto formCreateDto)
        {
            var result = await _requestFormService.CreateNewRequest(formCreateDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
