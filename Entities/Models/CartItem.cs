using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
