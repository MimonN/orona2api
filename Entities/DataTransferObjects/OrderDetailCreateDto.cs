using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OrderDetailCreateDto
    {
        [Required]
        public Guid OrderHeaderId { get; set; }

        [Required]
        public int ProductId { get; set; } 
        [Required]
        public int Count { get; set; }
    }
}
