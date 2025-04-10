using DataAccess.DTO;
using BusinessObject.Entities;

namespace Service.Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<MemberDTO>> GetAllMembersAsync();
        Task<MemberDTO> GetMemberByIdAsync(int id);
        Task<MemberDTO> GetMemberByEmailAsync(string email);
        Task<MemberDTO?> LoginAsync(string email, string password);
        Task<bool> IsAdmin(MemberDTO member);
        Task AddMemberAsync(MemberDTO member);
        Task UpdateMemberAsync(MemberUpdateDTO member);
        Task DeleteMemberAsync(int id);
        Task<List<Member>> GetAllsAsync();
    }
}
