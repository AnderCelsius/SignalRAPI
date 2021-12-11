using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Serilog;
using SignalRAPI.Core.Interfaces;
using SignalRAPI.Dtos;
using SignalRAPI.Dtos.UserDtos;
using SignalRAPI.Models;
using SignalRAPI.Utilities;
using SignalRAPI.Utilities.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRAPI.Core.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserService(UserManager<AppUser> userManager, 
            IMapper mapper, ILogger logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<UserReadDto>> GetUserById(string userId)
        {
            _logger.Information($"Attempting to fetch user with Id = {userId}");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.Information($"Search completed with no result");
                return Response<UserReadDto>.Fail($"No records with user_Id = {userId} in database");
            }

            _logger.Information($"Fetch successful");
            var response = _mapper.Map<UserReadDto>(user);
            return Response<UserReadDto>.Success("", response);
        }

        public async Task<Response<PaginatorHelper<IEnumerable<UserReadDto>>>> GetAll(PagingDto paging)
        {
            _logger.Information("Attempting to fetch all users");
            var users = _userManager.Users;
            var response = await users.PaginateAsync<AppUser, UserReadDto>(paging.PageSize, paging.PageNumber, _mapper);
            return Response<PaginatorHelper<IEnumerable<UserReadDto>>>.Success("Here are the list of users", response);
        }

        public async Task<Response<UserReadDto>> GetUserByEmail(string email)
        {
            _logger.Information($"Attempting to fetch user with Id = {email}");
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.Information($"Search completed with no result");
                return Response<UserReadDto>.Fail($"No records with user_Id = {email} in database");
            }

            _logger.Information($"Fetch successful");
            var response = _mapper.Map<UserReadDto>(user);
            return Response<UserReadDto>.Success("", response);
        }
    }
}
