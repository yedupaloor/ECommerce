using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Repository
{
    public interface IProductsRepository
    {
        Task<(bool isSuccessful, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync();
        Task<(bool isSuccessful, Models.Product product, string ErrorMessage)> GetProductAsync(int id);
    }
}
