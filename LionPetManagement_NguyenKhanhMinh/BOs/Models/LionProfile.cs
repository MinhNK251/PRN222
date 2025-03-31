using BusinessObjectLayer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOs.Models;

public partial class LionProfile
{
    [Required]
    public int LionProfileId { get; set; }
    [Required]
    public int LionTypeId { get; set; }
    [Required]
    [ValidateLionName]
    public string LionName { get; set; } = null!;
    [Required]
    public double Weight { get; set; }
    [Required]
    public string Characteristics { get; set; } = null!;
    [Required]
    public string Warning { get; set; } = null!;
    [Required]
    public DateTime ModifiedDate { get; set; }

    public virtual LionType? LionType { get; set; }
}
