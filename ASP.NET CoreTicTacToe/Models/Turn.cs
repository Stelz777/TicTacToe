using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreTicTacToe.Models
{
    public class Turn
    {
        public int CellNumber { get; set; }
        public Side WhichTurn { get; set; } 

        [Key]
        public Guid ID { get; set; }
    }
}
