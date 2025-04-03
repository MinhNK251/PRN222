using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string Country { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Password { get; set; } = null!;

        // Navigation property
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}