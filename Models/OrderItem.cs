using System;
namespace MyShop.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public int ItemId { get; set; }
		public Item Item { get; set; } = default!;
        public int Quantity { get; set; }
        public int OrderId { get; set; }
		public Order Order { get; set; } = default!;
		public decimal OrderItemPrice { get; set; }
    }
}

