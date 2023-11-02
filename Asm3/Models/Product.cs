using System;
using System.Collections.Generic;

namespace Asm3.Models
{
    public partial class Product
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitInStock { get; set; }
    }
}
