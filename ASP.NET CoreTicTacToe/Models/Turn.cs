using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Turn
    {
        public int CellNumber { get; set; }
        public Side WhichTurn { get; set; } 

        [Key]
        public Guid ID { get; set; }
    }
}
