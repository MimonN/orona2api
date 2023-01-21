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
    public class EstimateRequestRepository : RepositoryBase<EstimateRequest>, IEstimateRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public EstimateRequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(EstimateRequest obj)
        {
            _db.EstimateRequests.Update(obj);
            await _db.SaveChangesAsync();
        }

    }
}
