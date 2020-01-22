using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class TicTacToeRulesHelper
    {
        public static bool HasWinner(IReadOnlyList<Cell> squares)
        {
            if (CheckColumnsWinCondition(squares) || CheckRowsWinCondition(squares) || CheckDiagonalWinConditions(squares))
            {
                return true;
            }
            return false;
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
    }
}
