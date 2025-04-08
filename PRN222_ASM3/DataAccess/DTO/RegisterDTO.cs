using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
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

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
