using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OrderHeaderUpdateDto
    {
        public DateTime? ShippingDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? Carrier { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Note { get; set; }
    }
}
