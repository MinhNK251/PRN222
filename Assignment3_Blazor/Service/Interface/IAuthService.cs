using Service.DTO;

namespace Service.Interface
{
    public interface IAuthService
    {
        Task<bool> ValidateAdminCredentials(LoginDTO loginDTO);
        Task<bool> ValidateMemberCredentials(MemberLoginDTO loginModel);
        Task<MemberDTO> RegisterMember(MemberRegisterDTO registerModel);
        Task<bool> IsEmailRegistered(string email);
    }
}