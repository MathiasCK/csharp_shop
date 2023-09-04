using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using MyShop.DAL;
using MyShop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    public class ItemController : Controller
    {
        
        private readonly ItemRepository _itemRepository;

        public ItemController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public List<Order> OrderConsole()
        {
            return _itemRepository.OrderConsole();
        }

        public async Task<IActionResult> Grid()
        {
            var items = await _itemRepository.GetAll();
            var itemListViewModel = new ItemListViewModel(items, "Grid");

            return View(itemListViewModel);
        }


        public async Task<IActionResult> Table()
        {
            var items = await _itemRepository.GetAll();
            var itemListViewModel = new ItemListViewModel(items, "Items");

            return View(itemListViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemRepository.GetItemById(id);

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
                await _itemRepository.Create(item);
                return RedirectToAction(nameof(Table));
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _itemRepository.GetItemById(id);
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
                await _itemRepository.Update(item);
                return RedirectToAction(nameof(Table));
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemRepository.GetItemById(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _itemRepository.Delete(id);
            return RedirectToAction(nameof(Table));
            
        }
    }
}

