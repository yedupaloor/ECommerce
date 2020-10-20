using ECommerce.Api.Search.Models;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
