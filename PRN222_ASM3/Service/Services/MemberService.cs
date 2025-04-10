using DataAccess.DTO;
using BusinessObject.Entities;
using DataAccess.Repositories.Interfaces;
using Service.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IHubContext<ProductCategoryHub> _hub;
        private readonly IOptions<AdminAccountSettings> _adminAccountSettings;

        public MemberService(IMemberRepository memberRepository, IHubContext<ProductCategoryHub> hub, IOptions<AdminAccountSettings> adminAccountSettings)
        {
            _memberRepository = memberRepository;
            _hub = hub;
            _adminAccountSettings = adminAccountSettings;
        }

        public async Task<List<MemberDTO>> GetAllMembersAsync()
        {
            return await _memberRepository.GetAllAsync();
        }
      

        public async Task<MemberDTO> GetMemberByIdAsync(int id)
        {
            return await _memberRepository.GetByIdAsync(id);
        }

        public async Task<MemberDTO> GetMemberByEmailAsync(string email)
        {
            return await _memberRepository.GetMemberByEmailAsync(email);
        }

        public async Task<MemberDTO?> LoginAsync(string email, string password)
        {
            return await _memberRepository.LoginAsync(email, password, _adminAccountSettings);
        }

        public async Task<bool> IsAdmin(MemberDTO member)
        {
            return await _memberRepository.IsAdmin(member, _adminAccountSettings);
        }

        public async Task AddMemberAsync(MemberDTO member)
        {
            await _memberRepository.AddAsync(member);
            await _hub.Clients.All.SendAsync("MemberCreated", new MemberDTO
            {
                MemberId = member.MemberId,
                Email = member.Email,
                CompanyName = member.CompanyName,
                City = member.City,
                Country = member.Country
            });
        }

        public async Task UpdateMemberAsync(MemberUpdateDTO member)
        {
            await _memberRepository.UpdateAsync(member);
            await _hub.Clients.All.SendAsync("MemberUpdated", new MemberDTO
            {
                MemberId = member.MemberId,
                Email = member.Email,
                CompanyName = member.CompanyName,
                City = member.City,
                Country = member.Country
            });
        }

        public async Task DeleteMemberAsync(int id)
        {
            await _memberRepository.DeleteAsync(id);
            await _hub.Clients.All.SendAsync("MemberDeleted", id);
        }

        public async Task<List<Member>> GetAllsAsync()
        {
           return await _memberRepository.GetAllsAsync();
        }
    }
}
