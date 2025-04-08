using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class CategoryCreateDTO
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Description must not exceed 255 characters")]
        public string Description { get; set; } = string.Empty;
    }
}