using System;
using System.Collections.Generic;

namespace BOs
{
    public partial class SupplierCompany
    {
        public SupplierCompany()
        {
            OilPaintingArts = new HashSet<OilPaintingArt>();
        }

        public string SupplierId { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string CompanyTypeDescription { get; set; } = null!;
        public int? IsActive { get; set; }

        public virtual ICollection<OilPaintingArt> OilPaintingArts { get; set; }
    }
}
