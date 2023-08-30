﻿using System;
namespace MyShop.Models
{
	public class Item
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }

		public Item()
		{
		}
	}
}

