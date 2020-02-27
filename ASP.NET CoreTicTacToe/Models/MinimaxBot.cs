using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class MinimaxBot : IBot
    {
        public MinimaxBot(Side side)
        {
            Side = side;
        }

        public Side Side { get; private set; }

        public Turn MakeAutoMove(Game game)
        {
            if (game == null)
            {
                return null;
            }
            var bestMove = Minimax(game.Board);
            game.MakeMove(bestMove);
            return bestMove;
        }

        public Turn Minimax(Board board)
        {
            var bestMove = int.MinValue;
            var result = new Turn();
            result.Side = Side;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (board != null)
                    {
                        if (board.Squares[i * 3 + j] == Cell.Empty)
                        {
                            var simulatedBoard = Board.GenerateSimulatedBoard(board, i, j, Side);
                            var move = CalculateMinimumValue(simulatedBoard);
                            if (move > bestMove)
                            {
                                bestMove = move;
                                result.CellNumber = i * 3 + j;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private int CalculateMinimumValue(Board board)
        {
            if (board.HasWinner || !board.Squares.Contains(Cell.Empty))
            {
                if (board.HasWon(Side))
                {
                    return 1;
                }
                else if (!board.Squares.Contains(Cell.Empty))
                {
                    return 0;
                }
                return -1;
            }
            var utility = int.MaxValue;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (board.Squares[i * 3 + j] == Cell.Empty)
                    {
                        var simulatedBoard = Board.GenerateSimulatedBoard(
                            board, i, j, Side == Side.Tic ? Side.Tac : Side.Tic);

                        var move = CalculateMaximumValue(simulatedBoard);
                        if (move < utility)
                        {
                            utility = move;
                        }
                    }
                }
            }
            return utility;
        }

        public int CalculateMaximumValue(Board board)
        {
            if (board != null)
            {
                if (board.HasWinner || !board.Squares.Contains(Cell.Empty))
                {
                    if (board.HasWon(Side == Side.Tic ? Side.Tac : Side.Tic))
                    {
                        return -1;
                    }
                    else if (!board.Squares.Contains(Cell.Empty))
                    {
                        return 0;
                    }
                    return 1;
                }
            }
            var utility = int.MinValue;
            if (board != null)
            {
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (board.Squares[i * 3 + j] == Cell.Empty)
                        {
                            var simulatedBoard = Board.GenerateSimulatedBoard(board, i, j, Side);
                            var move = CalculateMinimumValue(simulatedBoard);
                            if (move > utility)
                            {
                                utility = move;
                            }
                        }
                    }
                }
            }
            return utility;
        }
    }
}
