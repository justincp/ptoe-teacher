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
    [Route("Quiz")]
    [ApiController]

    public class QuizController : Controller
    {
        private readonly QuizContext _db;

        public QuizController(QuizContext db)
        {
            _db = db;
        }


        [HttpPost]
        public async Task<ActionResult<int>> StoreResult(QuizResult result)
        {
            result.CreatedTime = DateTime.Now;
            result.IPaddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            _db.QuizResults.Attach(result);
            await _db.SaveChangesAsync();

            return result.QuizId;
        }


    }
}
