using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class MemberUpdateDTO
    {
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(40, ErrorMessage = "Company Name cannot exceed 40 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        [StringLength(15, ErrorMessage = "City cannot exceed 15 characters")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        [StringLength(15, ErrorMessage = "Country cannot exceed 15 characters")]
        public string Country { get; set; } = string.Empty;

        // Password không bắt buộc khi cập nhật
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30 characters")]
        public string? Password { get; set; }
    }
}
