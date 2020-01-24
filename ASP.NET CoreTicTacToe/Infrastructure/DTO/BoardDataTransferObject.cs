using ASP.NETCoreTicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Infrastructure.DTO
{
    public class BoardDataTransferObject
    {
        public Guid Id { get; set; }
        public string SerializedSquares { get; set; }
    }
}
