
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; } = string.Empty;
        
        public string? Description { get; set; }
    }
}