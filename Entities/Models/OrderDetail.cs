﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public Guid OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
