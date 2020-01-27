using System;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Models
{
    public class History
    {
        private List<Turn> turns = new List<Turn>();

        [System.ComponentModel.DataAnnotations.Key]
        public Guid ID { get; set; }

        public List<Turn> Turns => turns;

        public Turn LastTurn =>
             turns.Count > 0 ? turns[turns.Count - 1] : null;

        public void AddInvalidTurn()
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
                    board.SetSquare(turns[i].CellNumber, Board.GetCellBySide(turns[i].WhichTurn));
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

        public static Turn GetInvalidTurn()
        {
            return new Turn
            {
                CellNumber = -1,
                WhichTurn = Side.Tac
            };
        }
    }
}
