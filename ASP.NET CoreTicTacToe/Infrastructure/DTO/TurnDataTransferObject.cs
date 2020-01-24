using ASP.NETCoreTicTacToe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Infrastructure.DTO
{
    public class TurnDataTransferObject
    {
        public Guid Id { get; set; }
        
        public Guid HistoryId { get; set; }

        public int CellNumber { get; set; }

        public Cell WhichTurn { get; set; }

    }
}
