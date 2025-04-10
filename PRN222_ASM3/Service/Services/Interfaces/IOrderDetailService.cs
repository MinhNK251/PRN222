using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;

namespace Service.Services.Interfaces
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task<OrderDetail> GetOrderDetailAsync(int orderId, int productId);
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task DeleteOrderDetailAsync(int orderId, int productId);
    }
}
