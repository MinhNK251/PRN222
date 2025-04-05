// Service/DTO/OrderDTO.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        
        [Required(ErrorMessage = "Member is required")]
        public int MemberId { get; set; }
        
        public string? MemberEmail { get; set; }
        
        public string? MemberCompanyName { get; set; }
        
        [Required(ErrorMessage = "Order Date is required")]
        public DateTime OrderDate { get; set; }
        
        public DateTime? RequiredDate { get; set; }
        
        public DateTime? ShippedDate { get; set; }
        
        [Range(0, 1000, ErrorMessage = "Freight must be between 0 and 1000")]
        public decimal? Freight { get; set; }
        
        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
        
        public decimal TotalAmount => OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount));
    }
}