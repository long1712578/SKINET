using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product =await _storeContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _storeContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            return await _storeContext.ProductBrands
                .ToListAsync();
        }
        public async Task<IReadOnlyList<ProductType>> GetTypesAsync()
        {
            return await _storeContext.ProductTypes
                .ToListAsync();
        }
    }
}
