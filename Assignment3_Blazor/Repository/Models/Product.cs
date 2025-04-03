using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Weight { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        public int UnitsInStock { get; set; }

        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}