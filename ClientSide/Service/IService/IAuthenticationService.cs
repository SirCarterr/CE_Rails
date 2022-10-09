using Models;

namespace ClientSide.Service.IService
{
    public interface IAuthenticationService
    {
        Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO requestDTO);
        Task<SignInResponseDTO> LoginUser(SignInRequestDTO requestDTO);
        Task Logout();
    }
}
