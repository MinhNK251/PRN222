using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Services.Interfaces;
using DataAccess.DTO;
using BusinessObject.Entities;

namespace Service.Services
{
    public class LowStockBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LowStockBackgroundService> _logger;
        private readonly IHubContext<ProductCategoryHub> _hubContext;
        private const int Threshold = 10;
        private const int DelayInSeconds = 30;

        private readonly Dictionary<int, int> _lastAlertStocks = new();

        public LowStockBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<LowStockBackgroundService> logger,
            IHubContext<ProductCategoryHub> hubContext)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                    var products = await productService.GetAllProductsWithCategoryAsync();

                    var lowStockProducts = products
                        .Where(p => p.UnitsInStock < Threshold)
                        .ToList();

                    foreach (var product in lowStockProducts)
                    {
                        if (!_lastAlertStocks.ContainsKey(product.ProductId))
                        {
                            await SendAlert(product);
                        }
                        else
                        {
                            int lastStock = _lastAlertStocks[product.ProductId];
                            if (product.UnitsInStock < lastStock)
                            {
                                // update alert
                                await SendAlert(product);
                            }
                        }
                    }

                    var replenishedIds = _lastAlertStocks
                        .Where(kvp => products.FirstOrDefault(p => p.ProductId == kvp.Key)?.UnitsInStock >= Threshold)
                        .Select(kvp => kvp.Key)
                        .ToList();

                    foreach (var id in replenishedIds)
                    {
                        _lastAlertStocks.Remove(id);
                        _logger.LogInformation($"Stock replenished: Product ID {id} removed from low stock alerts.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking for low stock products.");
                }

                await Task.Delay(TimeSpan.FromSeconds(DelayInSeconds), stoppingToken);
            }
        }

        private async Task SendAlert(Product product)
        {
            var dto = new LowStockAlertDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock
            };

            await _hubContext.Clients.All.SendAsync("LowStockAlert", dto);
            _logger.LogWarning($"LOW STOCK ALERT: {dto.ProductName} ({dto.UnitsInStock} units left)");

            _lastAlertStocks[product.ProductId] = product.UnitsInStock;
        }
    }
}
