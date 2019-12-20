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
        public List<Cell> Squares { get; set; }
    }
}
