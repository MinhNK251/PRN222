// Service/DTO/ProductDTO.cs
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        
        public string? CategoryName { get; set; }
        
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(40, ErrorMessage = "Product Name cannot exceed 40 characters")]
        public string ProductName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Weight is required")]
        [StringLength(20, ErrorMessage = "Weight cannot exceed 20 characters")]
        public string Weight { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Unit Price must be greater than 0 and less than 10,000")]
        public decimal UnitPrice { get; set; }
        
        [Required(ErrorMessage = "Units In Stock is required")]
        [Range(0, 10000, ErrorMessage = "Units In Stock must be between 0 and 10,000")]
        public int UnitsInStock { get; set; }
    }
}