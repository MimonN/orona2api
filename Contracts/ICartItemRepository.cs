using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICartItemRepository : IRepositoryBase<CartItem>
    {
        Task UpdateAsync(CartItem obj);
        //Task<CartItem> ProductExistAsync(CartItem obj);
    }
}
