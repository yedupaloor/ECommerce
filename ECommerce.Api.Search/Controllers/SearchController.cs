using System.Threading.Tasks;
using ECommerce.Api.Search.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchServices _searchServices;

        public SearchController(ISearchServices searchServices)
        {
            _searchServices = searchServices;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> SearchAsync(int customerId)
        {
            var result = await _searchServices.SearchAsync(customerId);
            if(result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
    }
}
