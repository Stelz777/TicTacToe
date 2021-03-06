﻿using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Models
{
    public static class TicTacToeRulesHelper
    {
        public static bool HasWinner(IReadOnlyList<Cell> squares)
        {
            if (squares == null)
            {
                return false;
            }
            else
            {
                if (CheckColumnsWinCondition(squares) || CheckRowsWinCondition(squares) || CheckDiagonalWinConditions(squares))
                {
                    return true;
                }
                return false;
            }
        }

        private static bool CheckColumnsWinCondition(IReadOnlyList<Cell> squares)
        {
            for (var i = 0; i < 9; i += 3)
            {
                if (squares[i] != Cell.Empty && squares[i] == squares[i + 1] && squares[i + 1] == squares[i + 2])
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CheckRowsWinCondition(IReadOnlyList<Cell> squares)
        {
            for (var i = 0; i < 3; i++)
            {
                if (squares[i] != Cell.Empty && squares[i] == squares[i + 3] && squares[i + 3] == squares[i + 6])
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CheckDiagonalWinConditions(IReadOnlyList<Cell> squares)
        {
            if (squares[0] != Cell.Empty && squares[0] == squares[4] && squares[4] == squares[8])
            {
                return true;
            }
            if (squares[2] != Cell.Empty && squares[2] == squares[4] && squares[4] == squares[6])
            {
                return true;
            }
            return false;
        }

        public static bool HasWonVertically(IReadOnlyList<Cell> squares, Side side)
        {
            var cell = Board.GetCellBySide(side);
            if (squares != null)
            {
                if ((cell == squares[0] && squares[0] == squares[1] && squares[0] == squares[2]) ||
                    (cell == squares[3] && squares[3] == squares[4] && squares[3] == squares[5]) ||
                    (cell == squares[6] && squares[6] == squares[7] && squares[6] == squares[8]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasWonHorizontally(IReadOnlyList<Cell> squares, Side side)
        {
            var cell = Board.GetCellBySide(side);
            if (squares != null)
            {
                if ((cell == squares[0] && squares[0] == squares[3] && squares[0] == squares[6]) ||
                    (cell == squares[1] && squares[1] == squares[4] && squares[1] == squares[7]) ||
                    (cell == squares[2] && squares[2] == squares[5] && squares[2] == squares[8]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasWonDiagonally(IReadOnlyList<Cell> squares, Side side)
        {
            var cell = Board.GetCellBySide(side);
            if (squares != null)
            {
                if ((cell == squares[0] && squares[0] == squares[4] && squares[0] == squares[8]) ||
                    (cell == squares[2] && squares[2] == squares[4] && squares[2] == squares[6]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
