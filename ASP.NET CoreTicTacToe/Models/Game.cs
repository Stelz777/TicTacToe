﻿using System;
using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Game
    {
        public History History { get; set; }
        public Board Board { get; set; }

        private List<Player> players = new List<Player>();

        public List<Player> Players => players;

        public Game()
        {
            History = new History();
            Board = History.Turns[0];
            var realPlayer = new RealPlayer();
            var bot = new Bot(this);
            players.Add(realPlayer);
            players.Add(bot);
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
                    History.Turns.Add(newBoard);
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
                TicTurn = false
            };
        }

        
    }
}
