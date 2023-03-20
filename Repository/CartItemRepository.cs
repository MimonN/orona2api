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
    public class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
    {
        private readonly ApplicationDbContext _db;

        public CartItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public async Task<CartItem> ProductExistAsync(Product obj)
        //{
        //    var productExists = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.WindowType == obj.WindowType);
        //    return productExists;
        //}
        public async Task<CartItem> GetCartItemByUsernameAndProductId(string username, int ProductId)
        {
            var cartItem = _db.CartItems.FirstOrDefault(x => x.UserName == username && x.ProductId == ProductId);
            return cartItem;
        }
        public async Task UpdateAsync(CartItem obj)
        {
            _db.CartItems.Update(obj);
            await _db.SaveChangesAsync();
        }

    }
}
