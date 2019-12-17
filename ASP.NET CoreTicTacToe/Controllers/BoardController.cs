using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP.NET_CoreTicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;

        public BoardController(ILogger<BoardController> logger)
        {
            _logger = logger;
        }
        [Route("nextturn")]
        [HttpPost]
        public Turn NextTurn()
        {
            var random = new Random();
            int randomTurn = random.Next(1, 9);
            _logger.LogInformation($"Bot turn: { randomTurn }");
            return new Turn
            {
                CellNumber = randomTurn
            };
        }
    }
}