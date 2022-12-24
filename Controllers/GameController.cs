using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTOEQuiz.Data;

namespace PTOEQuiz.Controllers
{
    [Route("game")]
    [ApiController]

    public class GameController : Controller
    {
        private readonly GameContext _db;

        public GameController(GameContext db)
        {
            _db = db;
        }


        [HttpPost]
        public async Task<ActionResult<int>> StoreResult(GameResult result)
        {
            result.CreatedTime = DateTime.Now;
            //result.GameId = new Random().Next(10000);

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


    }
}
