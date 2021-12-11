using AutoMapper;
using SignalRAPI.Dtos.RequestFormDtos;
using SignalRAPI.Dtos.UserDtos;
using SignalRAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRAPI.Utilities.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<RequestForm, RequestFormReadDto>()
                .ForMember(dest => dest.Requester, y => y.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Status, y => y.MapFrom(src => src.FormStatus.Status));
            CreateMap<RequestFormCreateDto, RequestForm>();
            CreateMap<AppUser, UserReadDto>();
        }
    }
}
