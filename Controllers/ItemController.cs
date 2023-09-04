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
        private readonly ItemDbContext _itemDbContext;

        public ItemController(ItemDbContext itemDbContext)
        {
            _itemDbContext = itemDbContext;
        }

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

        public IActionResult Details(int id)
        {
            var items = GetItems();
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        public List<Item> GetItems()
        {
            return _itemDbContext.Items.ToList();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                _itemDbContext.Add(item);
                _itemDbContext.SaveChanges();
                return RedirectToAction(nameof(Table));
            }
            return View(item);
        }
    }
}

