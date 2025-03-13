using Sp25PharmaceuticalDB_BusinessObjects;
using Sp25PharmaceuticalDB_BusinessObjects.CustomValidation;
using System.ComponentModel.DataAnnotations;

public partial class MedicineInformation
{
    [Required]
    public string MedicineId { get; set; } = null!;
    [Required]
    public string MedicineName { get; set; } = null!;

    [Required]
    [ValidateActiveIngredients]
    public string ActiveIngredients { get; set; } = null!;
    [Required]
    public string? ExpirationDate { get; set; }
    [Required]
    public string DosageForm { get; set; } = null!;
    [Required]
    public string WarningsAndPrecautions { get; set; } = null!;
    [Required]
    public string? ManufacturerId { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }
}
