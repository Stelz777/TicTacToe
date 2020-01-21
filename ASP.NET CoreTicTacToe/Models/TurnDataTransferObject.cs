using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class TurnDataTransferObject
    {
        public Guid Id { get; set; }

        public int CellNumber { get; set; }

        public int WhichTurn { get; set; }
    }
}
