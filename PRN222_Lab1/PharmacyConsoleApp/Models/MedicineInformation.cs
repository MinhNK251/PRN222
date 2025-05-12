using System;
using System.Collections.Generic;

namespace PharmacyConsoleApp.Models;

public partial class MedicineInformation
{
    public string MedicineId { get; set; } = null!;

    public string MedicineName { get; set; } = null!;

    public string ActiveIngredients { get; set; } = null!;

    public string? ExpirationDate { get; set; }

    public string DosageForm { get; set; } = null!;

    public string WarningsAndPrecautions { get; set; } = null!;

    public string? ManufacturerId { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }
}
