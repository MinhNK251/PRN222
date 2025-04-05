// Service/Services/OrderService.cs
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;
using Service.DTO;
using Service.Interface;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                MemberId = o.MemberId,
                MemberEmail = o.Member.Email,
                MemberCompanyName = o.Member.CompanyName,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate,
                Freight = o.Freight,
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    ProductName = od.Product.ProductName,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    Discount = od.Discount
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found");

            return new OrderDTO
            {
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                MemberEmail = order.Member.Email,
                MemberCompanyName = order.Member.CompanyName,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                Freight = order.Freight,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    ProductName = od.Product.ProductName,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    Discount = od.Discount
                }).ToList()
            };
        }

        public async Task<OrderDTO> UpdateOrderAsync(OrderDTO orderDTO)
        {
            var order = await _context.Orders.FindAsync(orderDTO.OrderId);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderDTO.OrderId} not found");

            // Validate if member exists
            var memberExists = await _context.Members.AnyAsync(m => m.MemberId == orderDTO.MemberId);
            if (!memberExists)
                throw new KeyNotFoundException($"Member with ID {orderDTO.MemberId} not found");

            order.MemberId = orderDTO.MemberId;
            order.OrderDate = orderDTO.OrderDate;
            order.RequiredDate = orderDTO.RequiredDate;
            order.ShippedDate = orderDTO.ShippedDate;
            order.Freight = orderDTO.Freight;

            await _context.SaveChangesAsync();
            return await GetOrderByIdAsync(order.OrderId);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found");

            _context.OrderDetails.RemoveRange(order.OrderDetails);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderDetailDTO> AddOrderDetailAsync(OrderDetailDTO orderDetailDTO)
        {
            // Validate if order exists
            var orderExists = await _context.Orders.AnyAsync(o => o.OrderId == orderDetailDTO.OrderId);
            if (!orderExists)
                throw new KeyNotFoundException($"Order with ID {orderDetailDTO.OrderId} not found");

            // Validate if product exists
            var product = await _context.Products.FindAsync(orderDetailDTO.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {orderDetailDTO.ProductId} not found");

            // Check if product is already in the order
            var existingOrderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderDetailDTO.OrderId && od.ProductId == orderDetailDTO.ProductId);

            if (existingOrderDetail != null)
                throw new InvalidOperationException($"Product with ID {orderDetailDTO.ProductId} is already in the order");

            // Check if there's enough stock
            if (product.UnitsInStock < orderDetailDTO.Quantity)
                throw new InvalidOperationException($"Not enough stock for product {product.ProductName}. Available: {product.UnitsInStock}");

            var orderDetail = new OrderDetail
            {
                OrderId = orderDetailDTO.OrderId,
                ProductId = orderDetailDTO.ProductId,
                UnitPrice = orderDetailDTO.UnitPrice,
                Quantity = orderDetailDTO.Quantity,
                Discount = orderDetailDTO.Discount
            };

            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            orderDetailDTO.ProductName = product.ProductName;
            return orderDetailDTO;
        }

        public async Task<OrderDetailDTO> UpdateOrderDetailAsync(OrderDetailDTO orderDetailDTO)
        {
            var orderDetail = await _context.OrderDetails
                .Include(od => od.Product)
                .FirstOrDefaultAsync(od => od.OrderId == orderDetailDTO.OrderId && od.ProductId == orderDetailDTO.ProductId);

            if (orderDetail == null)
                throw new KeyNotFoundException($"Order detail not found for Order ID {orderDetailDTO.OrderId} and Product ID {orderDetailDTO.ProductId}");

            // Check if there's enough stock
            if (orderDetail.Product.UnitsInStock < orderDetailDTO.Quantity)
                throw new InvalidOperationException($"Not enough stock for product {orderDetail.Product.ProductName}. Available: {orderDetail.Product.UnitsInStock}");

            orderDetail.UnitPrice = orderDetailDTO.UnitPrice;
            orderDetail.Quantity = orderDetailDTO.Quantity;
            orderDetail.Discount = orderDetailDTO.Discount;

            await _context.SaveChangesAsync();

            orderDetailDTO.ProductName = orderDetail.Product.ProductName;
            return orderDetailDTO;
        }

        public async Task DeleteOrderDetailAsync(int orderId, int productId)
        {
            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);

            if (orderDetail == null)
                throw new KeyNotFoundException($"Order detail not found for Order ID {orderId} and Product ID {productId}");

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
        }
        

        public async Task<List<SalesReportItemDTO>> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            // Đảm bảo EndDate bao gồm cả ngày cuối cùng
            endDate = endDate.AddDays(1).AddSeconds(-1);
    
            var orders = await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();

            var reportItems = orders.Select(o => new SalesReportItemDTO
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    MemberCompanyName = o.Member.CompanyName,
                    TotalAmount = o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount)),
                    TotalItems = o.OrderDetails.Sum(od => od.Quantity),
                    ShippingStatus = o.ShippedDate.HasValue ? "Shipped" : (o.RequiredDate.HasValue ? "Pending" : "Processing")
                })
                .OrderByDescending(o => o.TotalAmount)
                .ToList();

            return reportItems;
        }

        public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            // Đảm bảo EndDate bao gồm cả ngày cuối cùng
            endDate = endDate.AddDays(1).AddSeconds(-1);
    
            var totalSales = await _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .SelectMany(o => o.OrderDetails)
                .SumAsync(od => od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount));

            return totalSales;
        }

        public async Task<int> GetTotalOrdersAsync(DateTime startDate, DateTime endDate)
        {
            // Đảm bảo EndDate bao gồm cả ngày cuối cùng
            endDate = endDate.AddDays(1).AddSeconds(-1);
    
            var totalOrders = await _context.Orders
                .CountAsync(o => o.OrderDate >= startDate && o.OrderDate <= endDate);

            return totalOrders;
        }
        public async Task<List<OrderDTO>> GetOrdersByMemberIdAsync(int memberId)
        {
            var orders = await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.MemberId == memberId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                MemberId = o.MemberId,
                MemberEmail = o.Member.Email,
                MemberCompanyName = o.Member.CompanyName,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate,
                Freight = o.Freight,
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    ProductName = od.Product.ProductName,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    Discount = od.Discount
                }).ToList()
            }).ToList();
        }
        public async Task<OrderDTO> CreateOrderAsync(OrderDTO orderDTO)
        {
            // Kiểm tra member có tồn tại không
            var memberExists = await _context.Members.AnyAsync(m => m.MemberId == orderDTO.MemberId);
            if (!memberExists)
                throw new KeyNotFoundException($"Member with ID {orderDTO.MemberId} not found");

            // Tạo đơn hàng mới
            var order = new Order
            {
                MemberId = orderDTO.MemberId,
                OrderDate = orderDTO.OrderDate,
                RequiredDate = orderDTO.RequiredDate,
                ShippedDate = orderDTO.ShippedDate,
                Freight = orderDTO.Freight
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Thêm chi tiết đơn hàng nếu có
            if (orderDTO.OrderDetails != null && orderDTO.OrderDetails.Any())
            {
                foreach (var detailDTO in orderDTO.OrderDetails)
                {
                    // Kiểm tra sản phẩm có tồn tại không
                    var product = await _context.Products.FindAsync(detailDTO.ProductId);
                    if (product == null)
                        throw new KeyNotFoundException($"Product with ID {detailDTO.ProductId} not found");

                    // Kiểm tra số lượng tồn kho
                    if (product.UnitsInStock < detailDTO.Quantity)
                        throw new InvalidOperationException($"Not enough stock for product {product.ProductName}. Available: {product.UnitsInStock}");

                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = detailDTO.ProductId,
                        UnitPrice = detailDTO.UnitPrice,
                        Quantity = detailDTO.Quantity,
                        Discount = detailDTO.Discount
                    };

                    _context.OrderDetails.Add(orderDetail);

                    // Cập nhật số lượng tồn kho
                    product.UnitsInStock -= detailDTO.Quantity;
                }

                await _context.SaveChangesAsync();
            }

            return await GetOrderByIdAsync(order.OrderId);
        }
    }
}