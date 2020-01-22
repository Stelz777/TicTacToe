using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class SquareDataTransferObject
    {
        [Key]
        public int Guid { get; set; }
        public int Cell { get; set; }
    }
}
