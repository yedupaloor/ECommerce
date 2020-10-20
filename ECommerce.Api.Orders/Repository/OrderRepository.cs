using AutoMapper;
using ECommerce.Api.Orders.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly ILogger<OrderRepository> _logger;
        private readonly IMapper _mapper;

        public OrderRepository(OrderDbContext orderDbContext,
            ILogger<OrderRepository> logger, IMapper mapper)
        {
            _orderDbContext = orderDbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_orderDbContext.Orders.Any())
            {
                _orderDbContext.Orders.Add(
                    new Order
                    {
                        Id = 1,
                        CustomerId = 1,
                        Items = new List<OrderItem> {
                            new OrderItem{ Id = 1, ProductId = 1, Quantity = 10, UnitPrice = 10},
                            new OrderItem{ Id = 2, ProductId = 1, Quantity = 20, UnitPrice = 20},
                            new OrderItem{ Id = 3, ProductId = 1, Quantity = 30, UnitPrice = 30}
                        },
                        OrderDate = DateTime.Now,
                        Total = 60.0
                    });
                _orderDbContext.Orders.Add(
                    new Order
                    {
                        Id = 2,
                        CustomerId = 1,
                        Items = new List<OrderItem> {
                            new OrderItem{ Id = 4, ProductId = 2, Quantity = 40, UnitPrice = 40},
                            new OrderItem{ Id = 5, ProductId = 2, Quantity = 50, UnitPrice = 50},
                            new OrderItem{ Id = 6, ProductId = 2, Quantity = 60, UnitPrice = 60}
                        },
                        OrderDate = DateTime.Now,
                        Total = 150.0
                    });
                _orderDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccessful, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await _orderDbContext.Orders
                    .Include(o => o.Items)
                    .ToListAsync();
                if (orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (true, null, "No Data");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }
        public async Task<(bool IsSuccessful, Models.Order Order, string ErrorMessage)> GetOrderAsync(int orderId)
        {
            return await GetOrdersAsync<Db.Order>(o => o.Id == orderId);
        }

        private async Task<
            (bool IsSuccessful, 
            Models.Order Order, 
            string ErrorMessage)
            > GetOrdersAsync<T>(Expression<Func<Db.Order, bool>> predicate)
        {
            try
            {
                var order = await _orderDbContext.Orders
                    .Include(o => o.Items)
                    .FirstOrDefaultAsync(predicate, new System.Threading.CancellationToken(false));
                if (order != null)
                {
                    var result = _mapper.Map<Db.Order, Models.Order>(order);
                    return (true, result, null);
                }
                return (true, null, "No Data");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccessful, Models.Order Order, string ErrorMessage)> GetCustomerOrderAsync(int customerId)
        {
            return await GetOrdersAsync<Db.Order>(o => o.CustomerId == customerId);
        }

        public async Task<(bool IsSuccessful, IEnumerable<Models.Order> Order, string ErrorMessage)> GetCustomerOrdersAsync(int customerId)
        {
            try
            {
                var order = _orderDbContext.Orders
                    .Include(o => o.Items)
                    .Where(o => o.CustomerId == customerId).ToList();
                if (order != null)
                {
                    var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(order);
                    return (true, result, null);
                }
                return (true, null, "No Data");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }
    }
}
