using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}