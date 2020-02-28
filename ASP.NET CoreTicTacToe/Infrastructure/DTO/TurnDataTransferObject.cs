using ASP.NETCoreTicTacToe.Models;
using System;

namespace ASP.NETCoreTicTacToe.Infrastructure.DTO
{
    public class TurnDataTransferObject
    {
        public Guid Id { get; set; }

        public Guid HistoryDataTransferObjectId { get; set; }

        public int CellNumber { get; set; }

        public Cell Side { get; set; }
    }
}
