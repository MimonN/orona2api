using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICartItemRepository CartItem { get; }
        IEstimateRequestRepository EstimateRequest { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }

        Task SaveAsync();
    }
}
