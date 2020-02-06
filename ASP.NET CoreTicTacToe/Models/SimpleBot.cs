using ASP.NETCoreTicTacToe.Models;
using System;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Models
{
    public class SimpleBot : Player, IBot
    {
        public Game Game { get; set; }

        public SimpleBot(Game game)
        {
            Game = game;
            Name = "S1mple";
        }

        public SimpleBot(Game game, Side side)
            :this(game)
        {
            Side = side;
        }

        public Turn MakeAutoMove()
        {
            var board = new Board();
            board.SetSquares(Game.Board.Squares);
            var possibleTurns = new List<int>(board.GetEmptySquareIndexes());
            var random = new Random();
            int randomTurn = random.Next(0, possibleTurns.Count);

            if (possibleTurns.Count > 0)
            {
                if (!board.HasWinner)
                {
                    var validTurn = new Turn
                    {
                        CellNumber = Convert.ToInt32(possibleTurns[randomTurn]),
                        Side = Side
                    };
                    Game.MakeMove(validTurn);
                    return validTurn;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
