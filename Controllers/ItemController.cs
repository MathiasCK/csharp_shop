using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public List<Order> OrderConsole()
        {
            return _itemDbContext.Orders.ToList();
        }

        public async Task<IActionResult> Grid()
        {
            var items = await _itemDbContext.Items.ToListAsync();
            var itemListViewModel = new ItemListViewModel(items, "Grid");

            return View(itemListViewModel);
        }


        public async Task<IActionResult> Table()
        {
            var items = await _itemDbContext.Items.ToListAsync();
            var itemListViewModel = new ItemListViewModel(items, "Items");

            return View(itemListViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemDbContext.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                _itemDbContext.Add(item);
                await _itemDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _itemDbContext.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Item item)
        {
            if (ModelState.IsValid)
            {
                _itemDbContext.Items.Update(item);
                await _itemDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemDbContext.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _itemDbContext.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _itemDbContext.Remove(item);
            await _itemDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
            
        }
    }
}

