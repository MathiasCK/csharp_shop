﻿using System;
namespace MyShop.Models
{
	public class Item
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}

