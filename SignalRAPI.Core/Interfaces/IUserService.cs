using SignalRAPI.Dtos;
using SignalRAPI.Dtos.UserDtos;
using SignalRAPI.Utilities;
using SignalRAPI.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRAPI.Core.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserReadDto>> GetUserById(string userId);
        Task<Response<UserReadDto>> GetUserByEmail(string email);
        Task<Response<PaginatorHelper<IEnumerable<UserReadDto>>>> GetAll(PagingDto paging);
    }
}
