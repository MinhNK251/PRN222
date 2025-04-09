using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;
using DataAccess.Data;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly eStoreDuckContext _context;

        public OrderDetailRepository(eStoreDuckContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                .AsNoTracking()
                .Include(od => od.Product) 
                .Where(od => od.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetailAsync(int orderId, int productId)
        {
            return await _context.OrderDetails.AsNoTracking()
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            var existingOrderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderDetail.OrderId && od.ProductId == orderDetail.ProductId);

            if (existingOrderDetail != null)
            {
                // If OrderDetail existed, update its quantity
                existingOrderDetail.Quantity += orderDetail.Quantity;
                existingOrderDetail.Discount = orderDetail.Discount;

                // Make sure UnitPrice is updated if it's 0 but the incoming value is not 0
                if (existingOrderDetail.UnitPrice == 0 && orderDetail.UnitPrice > 0)
                {
                    existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
                }
            }
            else
            {
                // Ensure UnitPrice is set before adding
                if (orderDetail.UnitPrice == 0)
                {
                    // You might want to throw an exception or log a warning here
                    // because adding an order detail with price 0 is probably not intended
                    Console.WriteLine($"Warning: Adding order detail for product {orderDetail.ProductId} with UnitPrice 0");
                }

                await _context.OrderDetails.AddAsync(orderDetail);
            }

            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }


        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            var existingOrderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderDetail.OrderId && od.ProductId == orderDetail.ProductId);

            if (existingOrderDetail == null)
            {
                throw new Exception("OrderDetail not found.");
            }

          
            existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
            existingOrderDetail.Quantity = orderDetail.Quantity;
            existingOrderDetail.Discount = orderDetail.Discount;

            _context.OrderDetails.Update(existingOrderDetail);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        public async Task DeleteOrderDetailAsync(int orderId, int productId)
        {
            var detail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);

            if (detail == null)
            {
                throw new Exception($"OrderDetail with OrderId {orderId} and ProductId {productId} not found.");
            }

            _context.OrderDetails.Remove(detail);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear(); 
        }
    }
}
