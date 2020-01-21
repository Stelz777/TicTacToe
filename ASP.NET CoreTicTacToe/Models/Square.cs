using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Square
    {
        [Key]
        public int Guid { get; set; }
        public Cell Cell { get; set; }

        public Square()
        {
            Cell = Cell.Empty;
        }
    }
}
