using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public enum Cell
    {
        Cross,
        Nought,
        Empty
    }

    public class Board
    {
        private List<Cell> squares = new List<Cell>();
        public IReadOnlyList<Cell> Squares => squares;

        public Board()
        {
            squares.AddRange(Enumerable.Repeat(Cell.Empty, 9));
        }

        public Board Copy()
        {
            var result = new Board();
            result.squares = squares;
            return result;
        }

        public void SetSquare(int cellNumber, Cell square)
        {
            squares[cellNumber] = square;
        }

        public void SetSquares(IReadOnlyList<Cell> squares)
        {
            this.squares = new List<Cell>();
            foreach (var square in squares)
            {
                this.squares.Add(square);
            }
        }

        bool CheckColumnsWinCondition()
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

        bool CheckRowsWinCondition()
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

        bool CheckDiagonalWinConditions()
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

        public bool HasWinner()
        {
            if (CheckColumnsWinCondition() || CheckRowsWinCondition() || CheckDiagonalWinConditions())
            {
                return true;
            }
            return false;
        }

        public void CalculatePossibleTurns(ref List<int> possibleTurns)
        {
            for (var i = 0; i < squares.Count; i++)
            {
                if (squares[i] == Cell.Empty)
                {
                    possibleTurns.Add(i);
                }
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

        public Turn MakeAutoMove() 
        {
            var possibleTurns = new List<int>();
            CalculatePossibleTurns(ref possibleTurns);
           
            var random = new Random();
            int randomTurn = random.Next(0, possibleTurns.Count);

            if (possibleTurns.Count > 0)
            {
                if (!HasWinner())
                {
                    SetSquare(Convert.ToInt32(possibleTurns[randomTurn]), Cell.Nought);
                    return new Turn
                    {
                        CellNumber = Convert.ToInt32(possibleTurns[randomTurn]),
                        TicTurn = false
                    };
                }
                else
                {
                    return GetInvalidTurn();
                }
            }
            else
            {
                return GetInvalidTurn();
            }
        }

        public bool SetBoard(History history, Turn turn)
        {
            SetSquares(history.Turns[history.Turns.Count - 1].Squares);
            if (!HasWinner())
            {
                if (Squares[turn.CellNumber] == Cell.Empty)
                {
                    SetSquare(turn.CellNumber, Cell.Cross);
                    history.Turns.Add(this);
                    
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
