using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Product> ProductExistAsync(Product obj)
        {
            var productExists = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.WindowType == obj.WindowType);
            return productExists;
        }

        public async Task UpdateAsync(Product obj)
        {
            _db.Products.Update(obj);
            await _db.SaveChangesAsync();
        }

    }
}
