using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsDbContext _productsDbContext;
        private readonly ILogger<ProductsRepository> _logger;
        private readonly IMapper _mapper;

        public ProductsRepository(ProductsDbContext productsDbContext, ILogger<ProductsRepository> logger, IMapper mapper)
        {
            _productsDbContext = productsDbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_productsDbContext.Products.Any())
            {
                _productsDbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                _productsDbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 200 });
                _productsDbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 1000 });
                _productsDbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 2000 });
                _productsDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccessful, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _productsDbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (true, null, "Not Found!");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccessful, Models.Product product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _productsDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = _mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (true, null, "Not Found!");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
