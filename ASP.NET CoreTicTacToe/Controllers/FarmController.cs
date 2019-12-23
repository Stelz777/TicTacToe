using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_CoreTicTacToe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CoreTicTacToe.Controllers
{
    [Route("api/[controller]/[action]/{id}")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private Farm farm;

        public FarmController(Farm farm)
        {
            this.farm = farm;
        }

        [HttpGet]
        public IActionResult GetGame(int? id)
        {
            var (boardId, board) = farm.FindBoard(id);
            return Json(new {

                id = boardId, 
                board
            });
        }

    }
}