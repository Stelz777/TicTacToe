using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class HistoryDataTransferObject
    {
        public Guid Id { get; set; }
        public List<TurnDataTransferObject> Turns { get; set; }
    }
}
