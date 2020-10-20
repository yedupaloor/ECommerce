using AutoMapper;
using ECommerce.Api.Customers.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace ECommerce.Api.Customers.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly ILogger<CustomerRepository> _logger;
        private readonly IMapper _mapper;

        public CustomerRepository(CustomerDbContext customerDbContext, ILogger<CustomerRepository> logger, IMapper mapper)
        {
            _customerDbContext = customerDbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_customerDbContext.Customers.Any())
            {
                _customerDbContext.Customers.Add(new Customer { Id = 1, Name = "Customer 1", Address = "Address 1" });
                _customerDbContext.Customers.Add(new Customer { Id = 2, Name = "Customer 2", Address = "Address 2" });
                _customerDbContext.Customers.Add(new Customer { Id = 3, Name = "Customer 3", Address = "Address 3" });
                _customerDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccessful, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _customerDbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (true, null, "No Data");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccessful, Models.Customer customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _customerDbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    var result = _mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (true, null, "No Data");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
