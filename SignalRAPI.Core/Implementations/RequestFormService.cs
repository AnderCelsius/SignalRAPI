using AutoMapper;
using Serilog;
using SignalRAPI.Core.Interfaces;
using SignalRAPI.Data.UnitOfWork;
using SignalRAPI.Dtos;
using SignalRAPI.Dtos.RequestFormDtos;
using SignalRAPI.Models;
using SignalRAPI.Utilities;
using SignalRAPI.Utilities.ResourceFiles;
using SignalRAPI.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SignalRAPI.Core.Implementations
{
    public class RequestFormService : IRequestFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly UserManager<AppUser> _userManager;

        public RequestFormService(IUnitOfWork unitOfWork, 
            IMapper mapper, ILogger logger, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<Response<bool>> ApproveRequestForm(int formId)
        {
            _logger.Information($"Attempting to fetch record for form with Id = {formId}");
            var requestForm = await _unitOfWork.RequestForms.Get(q => q.Id == formId, includes: new List<string>() { "FormStatus", "User" });
            if (requestForm == null)
            {
                _logger.Information($"Search completed with no result");
                return Response<bool>.Fail($"No records with form_Id = {formId} in database");
            }

            _logger.Information($"Fetch successful. Attempting to update form status and save to db");
            requestForm.FormStatus.Status = RequestFormStatus.Approved;

            _unitOfWork.RequestForms.Update(requestForm);
            await _unitOfWork.Save();

            _logger.Information($"Update complete and saved to db");
            var response = _mapper.Map<bool>(requestForm);
            return Response<bool>.Success("Request approved successfully and forwarded to disburser", response);
        }

        public async Task<Response<RequestFormReadDto>> CreateNewRequest(RequestFormCreateDto requestForm, string userId)
        {
            if(requestForm == null)
            {
                return Response<RequestFormReadDto>.Fail($"Form cannot be null");
            }

            var user = await _userManager.FindByEmailAsync(userId);
            if (user == null)
            {
                return Response<RequestFormReadDto>.Fail($"User does not exist");
            }

            var newRequestForm = _mapper.Map<RequestForm>(requestForm);

            newRequestForm.FormStatus = new FormStatus()
            {
                Status = RequestFormStatus.NewRequest
            };

            newRequestForm.User = user;

            _logger.Information($"Attempting to add new form to database");
            _unitOfWork.RequestForms.Attach(newRequestForm);
            await _unitOfWork.Save();
            _logger.Information($"Form added to database");

            var response = _mapper.Map<RequestFormReadDto>(newRequestForm);

            return Response<RequestFormReadDto>.Success("", response);

        }

        public Task<Response<bool>> DeleteRequestForm(string userId, int formId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<RequestFormReadDto>> Get(int formId)
        {
            _logger.Information($"Attempting to fetch record for form with Id = {formId}");
            var requestForm = await _unitOfWork.RequestForms.Get(q => q.Id == formId, includes: new List<string>() { "FormStatus", "User"});
            if(requestForm == null)
            {
                _logger.Information($"Search completed with no result");
                return Response<RequestFormReadDto>.Fail($"No records with form_Id = {formId} in database");
            }

            _logger.Information($"Fetch successful");
            var response = _mapper.Map<RequestFormReadDto>(requestForm);
            return Response<RequestFormReadDto>.Success("", response);

        }

        public async Task<Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>> GetAll(PagingDto paging)
        {
            _logger.Information($"Attempting to fetch all records");
            var requestForms =  _unitOfWork.RequestForms.GetAll(includes: new List<string>() { "FormStatus", "User" });
            var paginatedRequestForms = await requestForms.PaginateAsync<RequestForm, RequestFormReadDto>(paging.PageSize, paging.PageNumber, _mapper);
            return Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>.Success("", paginatedRequestForms);
        }

        public async Task<Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>> GetFormByStatus(PagingDto paging, string formStatus)
        {
            _logger.Information($"Attempting to fetch records for form with status = {formStatus}");
            var requestForms = _unitOfWork.RequestForms.GetAll(includes: new List<string>() { "FormStatus", "User" });
            if (requestForms == null)
            {
                _logger.Information($"Search completed with no result");
                return Response< PaginatorHelper<IEnumerable<RequestFormReadDto>>>.Fail($"There are no form entries in db");
            }

            _logger.Information($"Fetch successful");
            var paginatedRequestForms = await requestForms.Where(q => q.FormStatus.Status == formStatus).PaginateAsync<RequestForm, RequestFormReadDto>(paging.PageSize, paging.PageNumber, _mapper);
            if (paginatedRequestForms == null)
            {
                _logger.Information($"Search completed with no result");
                return Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>.Fail($"No records with form_status = {formStatus} in database");
            }

            return Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>.Success("", paginatedRequestForms);
        }

        public async Task<Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>> GetRequestorRequestForms(PagingDto paging, string userId)
        {
            _logger.Information($"Attempting to fetch records for user with Id = {userId}");
            var requestForms = _unitOfWork.RequestForms.GetAll(includes: new List<string>() { "FormStatus", "User" }).Where(rf => rf.UserId == userId);
            if(requestForms != null)
            {
                _logger.Information($"Fetched all records successfully");
                var paginatedRequestForms = await requestForms.PaginateAsync<RequestForm, RequestFormReadDto>(paging.PageSize, paging.PageNumber, _mapper);
                return Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>.Success("", paginatedRequestForms);
            }

            _logger.Information($"No records found");
            return Response<PaginatorHelper<IEnumerable<RequestFormReadDto>>>.Fail("No records found for user");

        }

        public Task<Response<RequestFormReadDto>> RejectRequestForm(int formId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateRequestForm(int formId)
        {
            throw new NotImplementedException();
        }
    }
}
