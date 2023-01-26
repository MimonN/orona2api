using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OrderHeader
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }
        public string? Carrier { get; set; }
        public string? TrackingNumber { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
