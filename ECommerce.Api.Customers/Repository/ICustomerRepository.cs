using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Repository
{
    public interface ICustomerRepository
    {
        Task<(bool isSuccessful, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync();
        Task<(bool isSuccessful, Models.Customer customer, string errorMessage)> GetCustomerAsync(int id);

    }
}
