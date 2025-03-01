using System.ComponentModel.DataAnnotations;

namespace BOs
{
    public partial class OilPaintingArt
    {
        [Required(ErrorMessage = "Oil Painting Art Id is required")]
        public int OilPaintingArtId { get; set; }
        [Required(ErrorMessage = "Art Title is required")]
        [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Must begin capital letter")]
        public string ArtTitle { get; set; } = null!;
        [Required(ErrorMessage = "Art Description is required")]
        public string? OilPaintingArtLocation { get; set; }
        [Required(ErrorMessage = "Art Style is required")]
        public string? OilPaintingArtStyle { get; set; }
        [Required(ErrorMessage = "Art Dimension is required")]
        public string? Artist { get; set; }
        [Required(ErrorMessage = "Art Dimension is required")]
        public string? NotablFeatures { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal? PriceOfOilPaintingArt { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 50, ErrorMessage = "Quantity must be between 1 and 50")]
        public int? StoreQuantity { get; set; }
        [Required(ErrorMessage = "Created Date is required")]
        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "Supplier Id is required")]
        public string? SupplierId { get; set; }

        public virtual SupplierCompany? Supplier { get; set; }
    }
}
