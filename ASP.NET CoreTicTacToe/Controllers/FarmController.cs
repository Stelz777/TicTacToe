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
        public RedirectResult GetGame(int id)
        {
            if (farm.Boards.ContainsKey(id))
            {
                farm.CurrentGame = id;
            }
            else
            {
                Board board = new Board();
                board.Squares = new List<Cell>();
                for (int i = 0; i < 9; i++)
                {
                    board.Squares.Add(Cell.Empty);
                }
                farm.Boards.Add(id, board);
                farm.CurrentGame = id;
            }
            return Redirect("/");
        }

    }
}