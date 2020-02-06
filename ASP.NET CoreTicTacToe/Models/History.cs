using System;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Models
{
    public class History
    {
        private List<Turn> turns = new List<Turn>();

        

        public List<Turn> Turns => turns;

        public Turn LastTurn =>
             turns.Count > 0 ? turns[turns.Count - 1] : null;

        public Board RestoreBoardByTurnNumber(int turnNumber)
        {
            if (turns.Count == 0)
            {
                return null;
            }
            var board = new Board();
            for (int i = 0; i <= turnNumber; i++)
            {
                if (turns[i].CellNumber >= 0)
                {
                    board.SetSquare(turns[i].CellNumber, Board.GetCellBySide(turns[i].Side));
                }
            }
            return board;
        }

        public List<Board> GetBoardsForEachTurn()
        {
            var boards = new List<Board>();
            if (turns != null)
            {
                for (var i = 0; i < Turns.Count; i++)
                {
                    boards.Add(RestoreBoardByTurnNumber(i));
                }
            }
            return boards;
        }
    }
}
