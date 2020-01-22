using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class BoardDataTransferObject
    {
        public Guid Id { get; set; }
        public List<SquareDataTransferObject> Squares { get; set; }
    }
}
