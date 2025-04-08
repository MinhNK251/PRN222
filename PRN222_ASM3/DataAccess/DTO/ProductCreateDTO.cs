using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Category ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be greater than 0")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 100 characters")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Weight is required")]
        [StringLength(50, ErrorMessage = "Weight must not exceed 50 characters")]
        public string Weight { get; set; } = string.Empty;

        [Required(ErrorMessage = "Unit price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Units in stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Units in stock must be 0 or more")]
        public int UnitsInStock { get; set; }
    }
}