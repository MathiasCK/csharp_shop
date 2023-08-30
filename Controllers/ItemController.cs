using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    public class ItemController : Controller
    {
        
        public IActionResult Table()
        {
            var items = new List<Item>();

            var item1 = new Item
            {
                Id = 1,
                Name = "Pizza",
                Price = 60
            };

            items.Add(item1);

            ViewBag.CurrentViewName = "Items in cart";

            return View(items);
        }
    }
}

