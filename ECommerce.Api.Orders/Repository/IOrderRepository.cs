using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Repository
{
    public interface IOrderRepository
    {
        Task<(bool IsSuccessful, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync();
        Task<(bool IsSuccessful, Models.Order Order, string ErrorMessage)> GetOrderAsync(int id);
        Task<(bool IsSuccessful, IEnumerable<Models.Order> Order, string ErrorMessage)> GetCustomerOrdersAsync(int customerId);
    }
}
