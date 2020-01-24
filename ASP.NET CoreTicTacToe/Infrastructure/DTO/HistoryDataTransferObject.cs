using ASP.NETCoreTicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Infrastructure.DTO
{
    public class HistoryDataTransferObject
    {
        public Guid Id { get; set; }
        public List<TurnDataTransferObject> Turns { get; set; }
    }
}
