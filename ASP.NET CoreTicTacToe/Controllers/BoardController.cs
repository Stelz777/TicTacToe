using ASP.NET_CoreTicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ASP.NET_CoreTicTacToe.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private Board board;

        private readonly ILogger<BoardController> _logger;

        public BoardController(Farm farm, ILogger<BoardController> logger)
        {
            int currentGame = farm.CurrentGame;
            board = farm.Boards[farm.CurrentGame];
            _logger = logger;
        }

        //[HttpPost]
        public Turn NextTurn()
        {
            List<int> possibleTurns = new List<int>();
            for (int i = 0; i < board.Squares.Count; i++)
            {
                if (board.Squares[i] != Cell.Cross && board.Squares[i] != Cell.Nought)
                {
                    possibleTurns.Add(i);
                }
            }
            var random = new Random();
            int randomTurn = random.Next(0, possibleTurns.Count);

            _logger.LogInformation($"Bot turn: { randomTurn }");
            if (possibleTurns.Count > 0)
            {
                return new Turn
                {
                    CellNumber = possibleTurns[randomTurn],
                    TicTurn = false
                };
            }
            else
            {
                return new Turn
                {
                    CellNumber = -1,
                    TicTurn = false
                };
            }
        }



        [HttpPost]
        public void SetBoard(Turn turn)
        {
            if (!board.HasWinner(board))
            {
                if (board.Squares[turn.CellNumber] == Cell.Empty)
                {

                    board.Squares[turn.CellNumber] = Cell.Cross;

                    if (!board.HasWinner(board))
                    {
                        Turn nextTurn = NextTurn();
                        if (nextTurn.CellNumber >= 0)
                        {
                            board.Squares[nextTurn.CellNumber] = Cell.Nought;
                        }
                    }
                }
            }
        }

        [HttpPost]
        public Board GetBoard()
        {
            return board;
        }
    }
}