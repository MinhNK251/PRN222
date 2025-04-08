namespace DataAccess.DTO
{
    public class LowStockAlertDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int UnitsInStock { get; set; }
    }
}