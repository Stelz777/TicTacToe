using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class TicTacToeRulesHelper
    {
        public static bool HasWinner(IReadOnlyList<Square> squares)
        {
            if (CheckColumnsWinCondition(squares) || CheckRowsWinCondition(squares) || CheckDiagonalWinConditions(squares))
            {
                return true;
            }
            return false;
        }

        private static bool CheckColumnsWinCondition(IReadOnlyList<Square> squares)
        {
            for (var i = 0; i < 9; i += 3)
            {
                if (squares[i].Cell != Cell.Empty && squares[i].Cell == squares[i + 1].Cell && squares[i + 1].Cell == squares[i + 2].Cell)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CheckRowsWinCondition(IReadOnlyList<Square> squares)
        {
            for (var i = 0; i < 3; i++)
            {
                if (squares[i].Cell != Cell.Empty && squares[i].Cell == squares[i + 3].Cell && squares[i + 3].Cell == squares[i + 6].Cell)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CheckDiagonalWinConditions(IReadOnlyList<Square> squares)
        {
            if (squares[0].Cell != Cell.Empty && squares[0].Cell == squares[4].Cell && squares[4].Cell == squares[8].Cell)
            {
                return true;
            }
            if (squares[2].Cell != Cell.Empty && squares[2].Cell == squares[4].Cell && squares[4].Cell == squares[6].Cell)
            {
                return true;
            }
            return false;
        }
    }
}
