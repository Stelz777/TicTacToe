using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class BoardForClient
    {
        public List<Cell> Squares;

        public void FillSquaresFromSource(Board source)
        {
            Squares = new List<Cell>();
            foreach (var square in source.Squares)
            {
                Squares.Add(square.Cell);
            }
        }
    }
}
