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
    public class OrderHeaderRepository : RepositoryBase<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
            await _db.SaveChangesAsync();
        }

    }
}
