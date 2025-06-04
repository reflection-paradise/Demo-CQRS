using Domain.Entities;
using Domain.EntitiesM;
using Domain.Interfaces;
using Infrastructure.DbConnect;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MongoDbECommerceContext _mongoContext;
        private readonly EcommerceDbContext _context;

        public ProductRepository(MongoDbECommerceContext mongoContext, EcommerceDbContext context)
        {
            _mongoContext = mongoContext;
            _context = context;
        }

        public async Task<Product> CreateRepository(Product data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteRepository(Product data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductM>> GetAllAsync()
        {
            return await _mongoContext.Products.Find(_ => true).ToListAsync();
        }

        public Task<ProductM?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateRepository(Product data)
        {
            _context.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}
