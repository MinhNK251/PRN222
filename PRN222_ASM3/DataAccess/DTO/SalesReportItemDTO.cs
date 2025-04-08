using System;

namespace DataAccess.DTO
{
    public class SalesReportItemDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string MemberCompanyName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}