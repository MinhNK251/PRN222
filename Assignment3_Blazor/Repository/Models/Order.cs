using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }

        // Navigation properties
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}