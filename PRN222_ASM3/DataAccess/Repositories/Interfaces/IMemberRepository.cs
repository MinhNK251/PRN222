using DataAccess.DTO;
using BusinessObject.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<MemberDTO>> GetAllAsync();
        Task<List<Member>> GetAllsAsync();
        Task<MemberDTO> GetByIdAsync(int id);
        Task<MemberDTO> GetMemberByEmailAsync(string email);
        Task<MemberDTO?> LoginAsync(string email, string password, IOptions<AdminAccountSettings> adminAccountSettings);
        Task<bool> IsAdmin(MemberDTO member, IOptions<AdminAccountSettings> adminAccountSettings);
        Task AddAsync(MemberDTO member);
        Task UpdateAsync(MemberUpdateDTO member);
        Task DeleteAsync(int id);
    }
}
