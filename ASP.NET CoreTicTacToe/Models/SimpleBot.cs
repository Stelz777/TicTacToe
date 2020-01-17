using System;
using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class SimpleBot : IBot
    {
        public bool isActive;
        public Side side;
        public Game game;

        public SimpleBot(Game game)
        {
            this.game = game;
        }

        public void InitSide(Side side)
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
                    if (side == Side.Tic)
                    {
                        filler = Cell.Cross;
                    }
                    if (side == Side.Tac)
                    {
                        filler = Cell.Nought;
                    }
                    board.SetSquare(Convert.ToInt32(possibleTurns[randomTurn]), filler);
                    game.History.Boards.Add(board);
                    game.SetBoard(board);
                    //!!!
                    //передать turn в game, чтобы поменять board
                    //!!!
                    return new Turn
                    {
                        CellNumber = Convert.ToInt32(possibleTurns[randomTurn]),
                        WhichTurn = side
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
