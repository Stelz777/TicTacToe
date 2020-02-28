using System;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Infrastructure.DTO
{
    public class HistoryDataTransferObject
    {
        public Guid Id { get; set; }
        public List<TurnDataTransferObject> Turns { get; set; }
    }
}
