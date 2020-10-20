using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public interface ISearchServices
    {
        Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId);
    }
}
