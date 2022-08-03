﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetail
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductImage { get; set; }
        public int InStock { get; set; }
    }
}
