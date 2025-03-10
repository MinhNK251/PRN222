using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.DTOs
{
    public class SystemAccountDTO
    {
        public short AccountId { get; set; }

        public string? AccountName { get; set; } = null!;

        public string? AccountEmail { get; set; } = null!;

        public int? AccountRole { get; set; }

        public string? AccountPassword { get; set; } = null!;
    }
}
