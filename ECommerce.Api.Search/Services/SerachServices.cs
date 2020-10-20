using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchServices : ISearchServices
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomerService _customerService;

        public SearchServices(IOrdersService ordersService, 
            IProductsService productsService,
            ICustomerService customerService
            )
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await _ordersService.GetOrdersAsync(customerId);
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    var customerResult = await _customerService.GetCustomerAsync(order.CustomerId);
                    order.CustomerName = customerResult.Customer.Name;
                    foreach (var item in order.Items)
                    {
                        var productsResult = await _productsService.GetProductAsync(item.ProductId);
                        item.ProductName = productsResult.Product.Name;
                    }
                }
                var result = new
                {
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
