using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using MyShop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    public class ItemController : Controller
    {

        public IActionResult Grid()
        {
            var items = GetItems();
            var itemListViewModel = new ItemListViewModel(items, "Grid");

            return View(itemListViewModel);
        }


        public IActionResult Table()
        {
            var items = GetItems();
            var itemListViewModel = new ItemListViewModel(items, "Items");

            return View(itemListViewModel);
        }

        public List<Item> GetItems()
        {
            var items = new List<Item>();

            var item1 = new Item
            {
                Id = 1,
                Name = "Pizza",
                Price = 60,
                Description = "Desc",
                ImageUrl = "/images/pizza.jpg"
            };

            var item2 = new Item
            {
                Id = 2,
                Name = "Tacos",
                Price = 80,
                Description = "Desc",
                ImageUrl = "/images/tacos.jpg"
            };

            var item3 = new Item
            {
                Id = 3,
                Name = "Coke",
                Price = 20,
                Description = "Desc",
                ImageUrl = "/images/coke.jpg"
            };

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);

            return items;
        }
    }
}

