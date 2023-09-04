using System;
namespace MyShop.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string OrderDate { get; set; } = string.Empty;
        public int CustomerId { get; set; }
		public Customer Customer { get; set; } = default!;
        public List<OrderItem>? OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

