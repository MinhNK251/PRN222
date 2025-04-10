using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;
using DataAccess.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _orderDetailRepository.GetOrderDetailsByOrderIdAsync(orderId);
        }

        public async Task<OrderDetail> GetOrderDetailAsync(int orderId, int productId)
        {
            return await _orderDetailRepository.GetOrderDetailAsync(orderId, productId);
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            await _orderDetailRepository.UpdateOrderDetailAsync(orderDetail);
        }

        public async Task DeleteOrderDetailAsync(int orderId, int productId)
        {
            await _orderDetailRepository.DeleteOrderDetailAsync(orderId, productId);
        }
    }
}
