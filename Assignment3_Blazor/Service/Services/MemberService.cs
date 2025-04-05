using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;
using Service.DTO;
using Service.Interface;

namespace Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _context;

        public MemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MemberDTO>> GetAllMembersAsync()
        {
            var members = await _context.Members.ToListAsync();
            return members.Select(m => new MemberDTO
            {
                MemberId = m.MemberId,
                Email = m.Email,
                CompanyName = m.CompanyName,
                City = m.City,
                Country = m.Country,
                Password = m.Password
            }).ToList();
        }

        public async Task<MemberDTO> GetMemberByIdAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {id} not found");

            return new MemberDTO
            {
                MemberId = member.MemberId,
                Email = member.Email,
                CompanyName = member.CompanyName,
                City = member.City,
                Country = member.Country,
                Password = member.Password
            };
        }

        public async Task<MemberDTO> CreateMemberAsync(MemberDTO memberDTO)
        {
            // Check if email already exists
            if (await _context.Members.AnyAsync(m => m.Email == memberDTO.Email))
                throw new InvalidOperationException($"Email {memberDTO.Email} is already in use");

            var member = new Member
            {
                Email = memberDTO.Email,
                CompanyName = memberDTO.CompanyName,
                City = memberDTO.City,
                Country = memberDTO.Country,
                Password = memberDTO.Password // In a real app, hash this password
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            memberDTO.MemberId = member.MemberId;
            return memberDTO;
        }

       
        public async Task<MemberDTO> UpdateMemberAsync(MemberUpdateDTO memberDTO)
        {
            var member = await _context.Members.FindAsync(memberDTO.MemberId);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {memberDTO.MemberId} not found");

            // Check if email is changed and already exists
            if (member.Email != memberDTO.Email && 
                await _context.Members.AnyAsync(m => m.Email == memberDTO.Email))
                throw new InvalidOperationException($"Email {memberDTO.Email} is already in use");

            member.Email = memberDTO.Email;
            member.CompanyName = memberDTO.CompanyName;
            member.City = memberDTO.City;
            member.Country = memberDTO.Country;
    
            // Only update password if it's provided
            if (!string.IsNullOrEmpty(memberDTO.Password))
            {
                member.Password = memberDTO.Password; // In a real app, hash this password
            }

            await _context.SaveChangesAsync();

            return new MemberDTO
            {
                MemberId = member.MemberId,
                Email = member.Email,
                CompanyName = member.CompanyName,
                City = member.City,
                Country = member.Country,
                Password = member.Password
            };
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {id} not found");

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }
        public async Task<MemberDTO> GetMemberByEmailAsync(string email)
        {
            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Email == email);

            if (member == null)
                throw new KeyNotFoundException($"Member with email {email} not found");

            return new MemberDTO
            {
                MemberId = member.MemberId,
                Email = member.Email,
                CompanyName = member.CompanyName,
                City = member.City,
                Country = member.Country
            };
        }
    }
}