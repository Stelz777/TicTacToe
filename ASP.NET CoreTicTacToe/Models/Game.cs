using ASP.NETCoreTicTacToe.Models;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ASP.NETCoreTicTacToe.Models
{
    public class Game
    {
        public History History { get; set; }
        public Board Board { get; private set; }
        public Player TicPlayer { get; set; }
        public Player TacPlayer { get; set; }

        public Game()
        {
            TicPlayer = new Player();
            TicPlayer.Side = Side.Tic;
            TacPlayer = new Player();
            TacPlayer.Side = Side.Tac;
            InitHistory();
            InitBoard();
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

        public bool CanContinue()
        {
            if (Board.HasWinner)
            {
                return false;
            }
            foreach (var cell in Board.Squares)
            {
                if (cell == Cell.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        public Side SetName(string name)
        {
            if (TicPlayer.Name == null)
            {
                TicPlayer.Name = name;
                TicPlayer.IsActive = true;
                return TicPlayer.Side;
            }
            else if (TacPlayer.Name == null)
            {
                TacPlayer.Name = name;
                return TacPlayer.Side;
            }
            
            throw new Exception("Only 2 players are supported.");
        }

        public bool MakeMove(Turn turn)
        {
            if (turn == null)
            {
                return false;
            }
           
            Turn lastTurn = History.LastTurn;

            if (lastTurn.Side == turn.Side)
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
                        newBoard.SetSquare(turn.CellNumber, Board.GetCellBySide(turn.Side));
                        History.Turns.Add(turn);
                            
                        Board = newBoard;

                        SetPlayersActivity(turn);

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

        public void SetPlayersActivity(Turn turn)
        {
            if (turn != null)
            {
                if (turn.Side == Side.Tic)
                {
                    TacPlayer.IsActive = true;
                    TicPlayer.IsActive = false;
                }
                else
                {
                    TacPlayer.IsActive = false;
                    TicPlayer.IsActive = true;
                }
            }
        }

        public Side GetSideByName(string name)
        {
            if (TicPlayer.Name != null)
            {
                if (TicPlayer.Name.Equals(name))
                {
                    return TicPlayer.Side;
                }
            }
            if (TacPlayer.Name != null)
            {
                if (TacPlayer.Name.Equals(name))
                {
                    return TacPlayer.Side;
                }
            }
            return Side.Tac;
        }

        public Player GetOpponent(string name)
        {
            if (TicPlayer.Name != null)
            {
                if (TicPlayer.Name.Equals(name))
                {
                    return TacPlayer;
                }
            }
            if (TacPlayer.Name != null)
            {
                if (TacPlayer.Name.Equals(name))
                {
                    return TicPlayer;
                }
            }
            return null;
        }
    }
}
