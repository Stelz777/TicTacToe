using System;
using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Game
    {
        //TODOпроверять очередность ходов, чтобы не было подряд двух крестов

        public History History { get; private set; }
        public Board Board { get; private set; }

        private RealPlayer player;

        private BotFarm botFarm = new BotFarm();

        public void SetBoard(Board board)
        {
            this.Board = board;
        }

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
                    newBoard.SetSquare(turn.CellNumber, Cell.Cross);
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
