using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza
{
    [Route("game")]
    [ApiController]

    public class GameController : Controller
    {
        private readonly PizzaStoreContext _db;

        public GameController(PizzaStoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
        {
            var orders = await _db.Orders
                .Include(o => o.Pizzas).ThenInclude(p => p.Special)
                .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
                .OrderByDescending(o => o.CreatedTime)
                .ToListAsync();

            return orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<int>> StoreResult(GameResult result)
        {
            result.CreatedTime = DateTime.Now;
           //result.GameId = new Random().Next(10000);
            
            Stupid s = new Stupid();
            s.OrderId = new Random().Next(10000);
            //s.UserId = "angry";
            _db.Stupids.Attach(s);
            _db.SaveChanges();

            _db.GameResults.Attach(result);
           // await _db.SaveChangesAsync();
            _db.SaveChanges();

            /*Order order = new Order();
            order.CreatedTime = DateTime.Now;
            order.OrderId = 1;
            order.UserId = "justin";
            order.DeliveryAddress = new Address();
            _db.Orders.Attach(order);
            await _db.SaveChangesAsync();
            */

            return result.GameId;
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderWithStatus>> GetOrderWithStatus(int orderId)
        {
            var order = await _db.Orders
                .Where(o => o.OrderId == orderId)
                .Include(o => o.Pizzas).ThenInclude(p => p.Special)
                .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
                .SingleOrDefaultAsync();
        
            if (order == null)
            {
                return NotFound();
            }
        
            return OrderWithStatus.FromOrder(order);
        }
    }
}
