using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ASP.NETCoreTicTacToe.Models
{
    public class Game
    {
        public History History { get; private set; }
        public Board Board { get; private set; }

        [Key]
        public int ID { get; set; }

        private RealPlayer player;

        public Game()
        {
            player = new RealPlayer();
        }

        public void InitHistory()
        {
            History = new History();
            History.AddInvalidTurn();
        }

        public void InitBoard()
        {
            Board = History.RestoreBoardByTurnNumber(0);
        }

        public bool MakeMove(Turn turn, DatabaseWorker databaseWorker)
        {
            if (turn == null)
            {
                return false;
            }
            else
            {
                Turn lastTurn = History.LastTurn;

                if (lastTurn.WhichTurn == turn.WhichTurn)
                {
                    return false;
                }
                else
                {
                    if (!Board.HasWinner)
                    {
                        if (Board.Squares[turn.CellNumber] == Cell.Empty)
                        {
                            Board newBoard = new Board();
                            newBoard.SetSquares(Board.Squares);
                            newBoard.SetSquare(turn.CellNumber, Board.GetCellBySide(turn.WhichTurn));
                            History.Turns.Add(turn);
                            if (databaseWorker != null)
                            {
                                databaseWorker.AddTurnToDatabase(turn, History);
                            }
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
            }
        }

        

        
    }
}
