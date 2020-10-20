using ECommerce.Api.Products.Db;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using ECommerce.Api.Products.Repository;
using AutoMapper;
using ECommerce.Api.Products.Profiles;
using System.Threading.Tasks;
using System.Linq;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsRepositoryTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var productsRepository = GetProductsRepository(nameof(GetProductsReturnsAllProducts));

            var result = await productsRepository.GetProductsAsync();

            Assert.True(result.isSuccessful);
            Assert.NotNull(result.products);
            Assert.Equal(10, result.products.Count());
            Assert.Null(result.ErrorMessage);
        }
        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var productsRepository = GetProductsRepository(nameof(GetProductReturnsProductUsingValidId));

            var result = await productsRepository.GetProductAsync(1);

            Assert.True(result.isSuccessful);
            Assert.NotNull(result.product);
            Assert.Null(result.ErrorMessage);
        }
        [Fact]
        public async Task GetProductReturnsNoProductUsingInValidId()
        {
            var productsRepository = GetProductsRepository(nameof(GetProductReturnsNoProductUsingInValidId));

            var result = await productsRepository.GetProductAsync(-1);

            Assert.True(result.isSuccessful);
            Assert.Null(result.product);
            Assert.NotNull(result.ErrorMessage);
        }

        private ProductsRepository GetProductsRepository(string dbName)
        {
            var dbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            var dbContext = new ProductsDbContext(dbContextOptions);
            CreateProducts(dbContext);

            var productsProfile = new ProductProfile();
            var mapperConfiguration = new MapperConfiguration(cnfg => cnfg.AddProfile(productsProfile));
            var mapper = new Mapper(mapperConfiguration);

            var productsRepository = new ProductsRepository(dbContext, null, mapper);
            return productsRepository;
        }
        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(
                    new Product 
                    { 
                        Id = i, 
                        Name = $"Product {i}", 
                        Inventory = i * 10, 
                        Price = (i * 10) + (int)new Random(10).Next() 
                    }
                );
            }
            dbContext.SaveChanges();
        }
    }
}
