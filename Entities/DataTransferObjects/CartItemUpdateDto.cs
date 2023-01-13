using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CartItemUpdateDto
    {
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
