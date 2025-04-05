// Service/Interface/IOrderService.cs
using Service.DTO;

namespace Service.Interface
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task<OrderDTO> UpdateOrderAsync(OrderDTO orderDTO);
        Task DeleteOrderAsync(int id);
        Task<OrderDetailDTO> AddOrderDetailAsync(OrderDetailDTO orderDetailDTO);
        Task<OrderDetailDTO> UpdateOrderDetailAsync(OrderDetailDTO orderDetailDTO);
        Task DeleteOrderDetailAsync(int orderId, int productId);
        Task<List<SalesReportItemDTO>> GetSalesReportAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate);
        Task<int> GetTotalOrdersAsync(DateTime startDate, DateTime endDate);
        Task<List<OrderDTO>> GetOrdersByMemberIdAsync(int memberId);
        Task<OrderDTO> CreateOrderAsync(OrderDTO orderDTO);
    }
}