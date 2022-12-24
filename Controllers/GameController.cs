using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            result.IPaddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            _db.GameResults.Attach(result);
            await _db.SaveChangesAsync();

            return result.GameId;
        }


    }
}
