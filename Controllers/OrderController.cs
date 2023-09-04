using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.ViewModels;

namespace MyShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ItemDbContext _itemDbContext;

        public OrderController(ItemDbContext itemDbContext)
        {
            _itemDbContext = itemDbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Order> orders = await _itemDbContext.Orders.ToListAsync();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrderItem()
        {
            var items = await _itemDbContext.Items.ToListAsync();
            var orders = await _itemDbContext.Orders.ToListAsync();
            var createOrderItemViewModel = new CreateOrderItemViewModel
            {
                OrderItem = new OrderItem(),
                ItemsSelectList = items.Select(item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Id.ToString() + " : " + item.Name,
                }).ToList(),
                OrdersSelectList = orders.Select(order => new SelectListItem
                {
                    Value = order.Id.ToString(),
                    Text = "Order: " + order.Id.ToString() + ", Date: " + order.OrderDate + ", Customer: " + order.Customer.Name,
                }).ToList()
            };

            return View(createOrderItemViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(OrderItem orderItem)
        {
            try
            {
                var newItem = _itemDbContext.Items.Find(orderItem.ItemId);
                var newOrder = _itemDbContext.Orders.Find(orderItem.OrderId);

                if (newItem == null || newOrder == null)
                {
                    return BadRequest("Item or order not found");
                }

                var newOrderItem = new OrderItem
                {
                    ItemId = orderItem.ItemId,
                    Item = newItem,
                    Quantity = orderItem.Quantity,
                    OrderId = orderItem.OrderId,
                    Order = newOrder,
                };

                newOrderItem.OrderItemPrice = orderItem.Quantity * newOrderItem.Item.Price;

                _itemDbContext.OrderItems.Add(newOrderItem);
                await _itemDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch
            {
                return BadRequest("Could not create OrderItem");
            };
        }
    }
}

