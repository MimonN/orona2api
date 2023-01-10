using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string WindowType { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1,100)]
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
