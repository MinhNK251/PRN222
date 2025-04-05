using Service.DTO;

namespace Service.Interface
{
    public interface IMemberService
    {
        Task<List<MemberDTO>> GetAllMembersAsync();
        Task<MemberDTO> GetMemberByIdAsync(int id);
        Task<MemberDTO> CreateMemberAsync(MemberDTO memberDTO);
        Task<MemberDTO> UpdateMemberAsync(MemberUpdateDTO memberDTO);
        Task<MemberDTO> GetMemberByEmailAsync(string email);
        Task DeleteMemberAsync(int id);
    }
}