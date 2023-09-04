﻿using System;
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
        private readonly ILogger<ItemController> _logger;

        public ItemController(ItemRepository itemRepository, ILogger<ItemController> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public List<Order> OrderConsole()
        {
            return _itemRepository.OrderConsole();
        }

        public async Task<IActionResult> Grid()
        {
            var items = await _itemRepository.GetAll();
            if (items == null)
            {
                _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
                return NotFound("Item list not found");
            }
            var itemListViewModel = new ItemListViewModel(items, "Grid");

            return View(itemListViewModel);
        }


        public async Task<IActionResult> Table()
        {
            var items = await _itemRepository.GetAll();
            if (items == null)
            {
                _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
                return NotFound("Item list not found");
            }
            var itemListViewModel = new ItemListViewModel(items, "Items");

            return View(itemListViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemRepository.GetItemById(id);

            if (item == null)
            {
                _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
                return NotFound("Item not found for the ItemId");
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
                bool returnOk = await _itemRepository.Create(item);
                if (returnOk)
                    return RedirectToAction(nameof(Table));
            }
            _logger.LogWarning("[ItemController] Item creation failed {@item}", item);
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _itemRepository.GetItemById(id);
            if (item == null)
            {
                _logger.LogError("[ItemController] Item not found when updating the ItemId {ItemId:0000}", id);
                return BadRequest("Item not found for the ItemId");
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Item item)
        {
            if (ModelState.IsValid)
            {
                bool returnOk = await _itemRepository.Update(item);
                if (returnOk)
                    return RedirectToAction(nameof(Table));
            }
            _logger.LogWarning("[ItemController] Item update failed {@item}", item);
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemRepository.GetItemById(id);
            if (item == null)
            {
                _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
                return BadRequest("Item not found for the ItemId");
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool returnOk = await _itemRepository.Delete(id);
            if (!returnOk)
            {
                _logger.LogError("[ItemController] Item deletion failed for the ItemId {ItemId:0000}", id);
                return BadRequest("Item deletion failed");
            }
            return RedirectToAction(nameof(Table));
        }
    }
}

