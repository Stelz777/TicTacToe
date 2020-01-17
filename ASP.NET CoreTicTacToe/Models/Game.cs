using System;
using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Game
    {
        //TODO 1
        //TODO после того, как история будет хранить ходы
        //TODOпроверять очередность ходов, чтобы не было подряд двух крестов

        public History History { get; private set; }
        public Board Board { get; private set; }

        private RealPlayer player;

        private BotFarm botFarm = new BotFarm();

        public Game()
        {
            History = new History();
            Board = History.Boards[0];
            player = new RealPlayer();
        }

        public bool MakeMove(Turn turn)
        {
            Board.SetSquares(Board.Squares);
            if (!Board.HasWinner)
            {
                if (Board.Squares[turn.CellNumber] == Cell.Empty)
                {
                    Board newBoard = new Board();
                    newBoard.SetSquares(Board.Squares);
                    if (turn.WhichTurn == Side.Tic)
                    {
                        newBoard.SetSquare(turn.CellNumber, Cell.Cross);
                    }
                    else if (turn.WhichTurn == Side.Tac)
                    {
                        newBoard.SetSquare(turn.CellNumber, Cell.Nought);
                    }
                    History.Boards.Add(newBoard);
                    Board = newBoard;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Turn GetInvalidTurn()
        {
            return new Turn
            {
                CellNumber = -1,
                WhichTurn = Side.Tac
            };
        }

        
    }
}
