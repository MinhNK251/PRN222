using Microsoft.Extensions.Configuration;
using Service.DTO;
using Service.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(IConfiguration configuration,ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public Task<bool> ValidateAdminCredentials(LoginDTO loginDTO)
        {
            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];

            bool isValid = loginDTO.Email == adminEmail && loginDTO.Password == adminPassword;

            return Task.FromResult(isValid);
        }
        public async Task<bool> ValidateMemberCredentials(MemberLoginDTO loginModel)
        {
            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Email.ToLower() == loginModel.Email.ToLower());

            if (member == null)
                return false;

            // Simple password comparison (no hashing)
            return member.Password == loginModel.Password;
        }

        public async Task<MemberDTO> RegisterMember(MemberRegisterDTO registerModel)
        {
            // Check if email already exists
            if (await IsEmailRegistered(registerModel.Email))
            {
                throw new InvalidOperationException("Email is already registered");
            }

            // Create new member
            var member = new Member
            {
                Email = registerModel.Email,
                CompanyName = registerModel.CompanyName,
                City = registerModel.City,
                Country = registerModel.Country,
                Password = registerModel.Password
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return new MemberDTO
            {
                MemberId = member.MemberId,
                Email = member.Email,
                CompanyName = member.CompanyName,
                City = member.City,
                Country = member.Country
            };
        }

        public async Task<bool> IsEmailRegistered(string email)
        {
            return await _context.Members.AnyAsync(m => m.Email.ToLower() == email.ToLower());
        }
    }
}