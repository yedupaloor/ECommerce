using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Profiles;
using ECommerce.Api.Customers.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.Customer.Tests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public async Task Should_Return_All_Customers_When_GetCustomersAsync_Called()
        {
            var customerRepository = GetCustomerRepository(nameof(Should_Return_All_Customers_When_GetCustomersAsync_Called));
            var result = await customerRepository.GetCustomersAsync();

            Assert.True(result.isSuccessful);
            Assert.Equal(10, result.customers.Count());
            Assert.Null(result.errorMessage);
        }

        [Fact]
        public async Task Should_Return_Customer_When_GetCustomerAsync_Called_With_Valid_Id()
        {
            var customerRepository = GetCustomerRepository(nameof(Should_Return_Customer_When_GetCustomerAsync_Called_With_Valid_Id));
            var result = await customerRepository.GetCustomerAsync(1);

            Assert.True(result.isSuccessful);
            Assert.Equal(1, result.customer.Id);
            Assert.Null(result.errorMessage);
        }

        [Fact]
        public async Task Should_Return_No_Customer_When_GetCustomerAsync_Called_With_InValid_Id()
        {
            var customerRepository = GetCustomerRepository(nameof(Should_Return_No_Customer_When_GetCustomerAsync_Called_With_InValid_Id));
            var result = await customerRepository.GetCustomerAsync(-1);

            Assert.True(result.isSuccessful);
            Assert.Null(result.customer);
            Assert.NotNull(result.errorMessage);
        }


        private CustomerRepository GetCustomerRepository(string dbName)
        {
            var dbContextOptions = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            var dbContext = new CustomerDbContext(dbContextOptions);
            CreateCustomers(dbContext);

            var productsProfile = new CustomerProfiles();
            var mapperConfiguration = new MapperConfiguration(cnfg => cnfg.AddProfile(productsProfile));
            var mapper = new Mapper(mapperConfiguration);

            var productsRepository = new CustomerRepository(dbContext, null, mapper);
            return productsRepository;
        }
        private void CreateCustomers(CustomerDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Customers.Add(
                    new Customers.Db.Customer
                    {
                        Id = i,
                        Name = $"Customer {i}"
                    }
                );
            }
            dbContext.SaveChanges();
        }
    }
}
