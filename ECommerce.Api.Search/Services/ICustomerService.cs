using ECommerce.Api.Search.Models;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
