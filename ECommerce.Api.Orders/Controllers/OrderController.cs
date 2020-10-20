using System.Threading.Tasks;
using ECommerce.Api.Orders.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("orders", Name ="orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            if (orders.IsSuccessful)
            {
                return Ok(orders.Orders);
            }
            return NotFound();
        }

        [HttpGet("orders/{id}", Name = "order")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            if (order.IsSuccessful)
            {
                return Ok(order.Order);
            }
            return NotFound();
        }

        [HttpGet("customerorders/{customerId}", Name = "customerorders")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var order = await _orderRepository.GetCustomerOrdersAsync(customerId);
            if (order.IsSuccessful)
            {
                return Ok(order.Order);
            }
            return NotFound();
        }

    }
}
