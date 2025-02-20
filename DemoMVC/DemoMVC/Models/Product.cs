using System;
using System.Collections.Generic;

namespace DemoMVC.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? UnitsInStock { get; set; }
}
