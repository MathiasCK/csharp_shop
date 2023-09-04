using System;
using Microsoft.EntityFrameworkCore;
using MyShop.Controllers;
using MyShop.Models;

namespace MyShop.DAL
{
	public class ItemRepository : IItemRepository
    {
        private readonly ItemDbContext _db;
        private readonly ILogger<ItemController> _logger;

        public ItemRepository(ItemDbContext db, ILogger<ItemController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<Item>?> GetAll()
        {
            try
            {
                return await _db.Items.ToListAsync();
            } catch(Exception e)
            {
                _logger.LogError("[ItemRepository] Items.ToListAsync() failed when GetAll(), error message: {e}", e.Message);
                return null;
            }
        }

        public async Task<Item?> GetItemById(int id)
        {
            try
            {
                return await _db.Items.FirstOrDefaultAsync(i => i.Id == id);
            } catch(Exception e)
            {
                _logger.LogError("[ItemRepository] Items.FirstOrDefaultAsync() failed when GetItemById() for ItemId {ItemId:0000}, error message: {e}", id, e.Message); ;
                return null;
            }
        }

        public async Task<bool> Create(Item item) {
            try
            {
                _db.Items.Add(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[ItemRepository] item creation failed for item {@item}, error message: {e}", item, e.Message); ;
                return false;
            }
        }

        public async Task<bool> Update(Item item) {
            try
            {
                _db.Items.Update(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[ItemRepository] Items.FindAsync(id) failed when updating ItemId {ItemId:0000}, error message: {e}", item, e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var item = await GetItemById(id);

                if (item == null)
                {
                    return false;
                }

                _db.Items.Remove(item);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception e)
            {

                _logger.LogError("[ItemRepository] deletion failed for ItemId {ItemId:0000}, error message: {e}", id, e.Message);
                return false;
            }
        }

        public List<Order> OrderConsole()
        {
            return _db.Orders.ToList();
        }
    }
}

