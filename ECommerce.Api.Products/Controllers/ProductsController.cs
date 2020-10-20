using ECommerce.Api.Products.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetProdutsAsync()
        {
            var result = await _productsRepository.GetProductsAsync();
            if(result.isSuccessful)
                return Ok(result.products);
            return NotFound(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutAsync(int id)
        {
            var result = await _productsRepository.GetProductAsync(id);
            if (result.isSuccessful)
                return Ok(result.product);
            return NotFound(result.ErrorMessage);
        }

    }
}
