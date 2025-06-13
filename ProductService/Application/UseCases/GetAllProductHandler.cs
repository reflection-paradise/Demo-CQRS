using Domain.EntitiesM;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetAllProductHandler 
    {
        private readonly IProductRepository _product;

        public GetAllProductHandler(IProductRepository product)
        {
            _product = product;
        }
        public async Task<List<ProductM>> GetAllProductAsync()
        {
            var products = await _product.GetAllAsync();
            return products;
        }
        public async Task<ProductM> GetProductByIdM(string id)
        {
            var products = await _product.GetByIdAsync(id);
            return products;
        }
    }
}
