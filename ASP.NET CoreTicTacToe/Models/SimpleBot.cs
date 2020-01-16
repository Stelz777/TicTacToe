using System;
using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class SimpleBot : IBot
    {
        public bool isActive;
        public string side;
        public Game game;

        public SimpleBot(Game game)
        {
            this.game = game;
        }

        public void InitSide(string side)
        {
            this.side = side;
        }

        public Turn MakeAutoMove()
        {
            var board = new Board();
            board.SetSquares(game.Board.Squares);
            var possibleTurns = new List<int>(board.GetEmptySquareIndexes());
            var random = new Random();
            int randomTurn = random.Next(0, possibleTurns.Count);

            if (possibleTurns.Count > 0)
            {
                if (!board.HasWinner)
                {
                    Cell filler = Cell.Empty;
                    bool isTicTurn = false;
                    if (side.Equals("Tic"))
                    {
                        filler = Cell.Cross;
                        isTicTurn = true;
                    }
                    if (side.Equals("Tac"))
                    {
                        filler = Cell.Nought;
                        isTicTurn = false;
                    }
                    board.SetSquare(Convert.ToInt32(possibleTurns[randomTurn]), filler);
                    game.History.Turns.Add(board);
                    game.Board = board;
                    return new Turn
                    {
                        CellNumber = Convert.ToInt32(possibleTurns[randomTurn]),
                        TicTurn = isTicTurn
                    };
                }
                else
                {
                    return game.GetInvalidTurn();
                }
            }
            else
            {
                return game.GetInvalidTurn();
            }
        }
    }
}
