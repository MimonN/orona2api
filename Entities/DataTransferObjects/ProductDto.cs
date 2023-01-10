﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string WindowType { get; set; }
        public string ImageUrl { get; set; }
    }
}
