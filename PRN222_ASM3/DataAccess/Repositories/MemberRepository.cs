using DataAccess.Data;
using DataAccess.DTO;
using BusinessObject.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
	{
		private readonly eStoreDuckContext _context;

		public MemberRepository(eStoreDuckContext context)
		{
			_context = context;
		}

		public async Task<List<MemberDTO>> GetAllAsync()
		{
            var members = await _context.Members
				.Include(m => m.Orders)
				.ToListAsync();
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

		public async Task<MemberDTO> GetByIdAsync(int id)
		{
            var member = await _context.Members
                .Include(m => m.Orders)
				.FirstOrDefaultAsync(m => m.MemberId == id);
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

        public async Task<MemberDTO?> LoginAsync(string email, string password, IOptions<AdminAccountSettings> adminAccountSettings)
        {
            var adminSettings = adminAccountSettings.Value;

            // Check if the provided credentials match the admin account
            if (email == adminSettings.Email && password == adminSettings.Password)
            {
                return new MemberDTO
                {
                    MemberId = 0, // Admin ID                    
                    Email = adminSettings.Email,
                    CompanyName = "FPT",
                    City = "HCM",
                    Country = "Viet Nam",
                    Password = adminSettings.Password,
                };
            }

            var member = await _context.Members
                    .FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
            if (member == null)
                return null;
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

        public async Task<bool> IsAdmin(MemberDTO member, IOptions<AdminAccountSettings> adminAccountSettings)
        {
            var adminSettings = adminAccountSettings.Value;
            if (member.Email == adminSettings.Email)
                return true;
            return false;
        }

        public async Task AddAsync(MemberDTO member)
		{
            if (await _context.Members.AnyAsync(m => m.Email == member.Email))
                throw new InvalidOperationException($"Email {member.Email} is already in use");

            var newMember = new Member
            {
                Email = member.Email,
                CompanyName = member.CompanyName,
                City = member.City,
                Country = member.Country,
                Password = member.Password
            };

            _context.Members.Add(newMember);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(MemberUpdateDTO member)
		{
            var updateMember = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == member.MemberId);
            _context.Members.Attach(updateMember);
            if (updateMember.Email != member.Email && await _context.Members.AnyAsync(m => m.Email == member.Email))
                throw new InvalidOperationException($"Email {member.Email} is already in use");

            updateMember.Email = member.Email;
            updateMember.CompanyName = member.CompanyName;
            updateMember.City = member.City;
            updateMember.Country = member.Country;
            if (!string.IsNullOrEmpty(member.Password))
            {
                updateMember.Password = member.Password;
            }
            _context.Members.Update(updateMember);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var member = await _context.Members.FirstOrDefaultAsync(m => m.MemberId == id);
            _context.Members.Attach(member);
            if (member != null)
			{
				_context.Members.Remove(member);
				await _context.SaveChangesAsync();
			}
		}

        public async Task<List<Member>> GetAllsAsync()
        {
            if (_context.Members == null)
            {
                return new List<Member>(); 
            }
            return await _context.Members.ToListAsync();
        }
    }
}
