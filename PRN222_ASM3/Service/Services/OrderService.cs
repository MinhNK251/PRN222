using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Entities;
using DataAccess.DTO;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IHubContext<ProductCategoryHub> _hub;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository    ,
            IHubContext<ProductCategoryHub> hub,
            ICategoryRepository categoryRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _hub = hub;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            
            if (order.OrderDetails != null && order.OrderDetails.Any())
            {
                foreach (var detail in order.OrderDetails)
                {
                    var product = await _productRepository.GetByIdAsync(detail.ProductId);
                    if (product == null)
                    {
                        throw new Exception($"Product with ID {detail.ProductId} not found.");
                    }

                    if (product.UnitsInStock < detail.Quantity)
                    {
                        throw new Exception($"Insufficient stock for product {product.ProductName}. Available: {product.UnitsInStock}, Requested: {detail.Quantity}");
                    }

                   
                    product.UnitsInStock -= detail.Quantity;
                    await _productRepository.UpdateAsync(product);


                    await _hub.Clients.All.SendAsync("ProductUpdated", new ProductSignalRDTO
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        CategoryId = product.CategoryId,
                        UnitPrice = product.UnitPrice,
                        Weight = product.Weight,
                        UnitsInStock = product.UnitsInStock,
                        CategoryName = await _categoryRepository.GetCategoryNameByIdAsync(product.CategoryId)
                    });
                }
            }

            await _orderRepository.AddOrderAsync(order);
            await _hub.Clients.All.SendAsync("OrderCreated", order.OrderId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
            await _hub.Clients.All.SendAsync("OrderDeleted", orderId);
        }

        public async Task<List<Order>> GetOrdersByMemberIdAsync(int memberId)
        {
            return await _orderRepository.GetOrdersByMemberIdAsync(memberId);
        }

        public async Task<List<SalesReportItemDTO>> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetSalesReportAsync(startDate, endDate);
        }

        public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetTotalSalesAsync(startDate, endDate);
        }

        public async Task<int> GetTotalOrdersAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetTotalOrdersAsync(startDate, endDate);
        }
    }
}