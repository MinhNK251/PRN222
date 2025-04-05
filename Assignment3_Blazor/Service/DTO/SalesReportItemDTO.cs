
using System;

namespace Service.DTO
{
    public class SalesReportItemDTO
    {
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public string MemberCompanyName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int TotalItems { get; set; }
        public string? ShippingStatus { get; set; }
    }
}