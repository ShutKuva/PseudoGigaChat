using Core.DTOs;

namespace BLL.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<string> Authenticate(UserDTOFromUI user);
    }
}
