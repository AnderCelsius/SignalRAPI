using SignalRAPI.Dtos;
using SignalRAPI.Dtos.RequestFormDtos;
using SignalRAPI.Utilities;
using SignalRAPI.Utilities.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRAPI.Core.Interfaces
{
    public interface IRequestFormService
    {
        Task<Response<RequestFormReadDto>> Get(int formId);
        Task<Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>> GetRequestorRequestForms(PagingDto paging, string userId);
        Task<Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>> GetAll(PagingDto paging);
        Task<Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>> GetFormByStatus(PagingDto paging, string formStatus);
        Task<Response<bool>> ApproveRequestForm(int formId);
        Task<Response<RequestFormReadDto>> RejectRequestForm(int formId);
        Task<Response<RequestFormReadDto>> CreateNewRequest(RequestFormCreateDto requestForm);
        Task<Response<bool>> DeleteRequestForm(string userId, int formId);
        Task<Response<bool>> UpdateRequestForm(int formId);

    }

}
