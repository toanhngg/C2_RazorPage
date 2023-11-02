using System;
using System.Collections.Generic;

namespace Asm3.Models
{
    public partial class Order
    {
        public string OrderId { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? CustomerId { get; set; }
        public decimal? TotalPayment { get; set; }
    }
}
