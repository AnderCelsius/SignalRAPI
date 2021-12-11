using SignalRAPI.Dtos.AuthenticationDtos;
using SignalRAPI.Utilities;
using System.Threading.Tasks;

namespace SignalRAPI.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Response<LoginResponse>> Login(LoginRequest model, string ipAddress);

    }
}
