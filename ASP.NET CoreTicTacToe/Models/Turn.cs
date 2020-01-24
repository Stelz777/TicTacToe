using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreTicTacToe.Models
{
    public class Turn
    {
        [Key]
        public Guid ID { get; set; }
        public int CellNumber { get; set; }
        public Side WhichTurn { get; set; } 
    }
}
