// Service/DTO/OrderDetailDTO.cs
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }
        
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }
        
        public string? ProductName { get; set; }
        
        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Unit Price must be greater than 0")]
        public decimal UnitPrice { get; set; }
        
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }
        
        [Range(0, 1, ErrorMessage = "Discount must be between 0 and 1")]
        public float Discount { get; set; }
    }
}