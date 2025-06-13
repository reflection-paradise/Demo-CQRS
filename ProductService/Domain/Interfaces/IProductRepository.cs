using Domain.Entities;
using Domain.EntitiesM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductM>> GetAllAsync();
        Task<ProductM?> GetByIdAsync(string id);
        Task<Product?> GetByIdSQLAsync(string id);

        Task<Product> CreateRepository(Product data);
        Task<Product> UpdateRepository(Product data);
        Task<bool> DeleteRepository(Product data);



    }
}
