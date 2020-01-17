﻿using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class History
    {
        private List<Turn> turns = new List<Turn>();

        public List<Turn> Turns
        {
            get
            {
                return turns;
            }
            set
            {
                turns = value;
            }
        }

        public History()
        {
            turns.Add(GetInvalidTurn());
        }

        public Board RestoreBoardByTurnNumber(int turnNumber)
        {
            var board = new Board();
            for (int i = 0; i <= turnNumber; i++)
            {
                if (turns[i].CellNumber >= 0)
                {
                    board.SetSquare(turns[i].CellNumber, board.GetCellBySide(turns[i].WhichTurn));
                }
            }
            return board;
        }

        public List<Board> GetBoardsForEachTurn()
        {
            var boards = new List<Board>();
            for (var i = 0; i < Turns.Count; i++)
            {
                boards.Add(RestoreBoardByTurnNumber(i));
            }
            return boards;
        }

        public Turn LastTurn =>
             turns[turns.Count - 1];
       

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
