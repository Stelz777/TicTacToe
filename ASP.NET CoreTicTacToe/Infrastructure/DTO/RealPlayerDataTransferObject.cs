using ASP.NETCoreTicTacToe.Models;
using System;

namespace ASP.NETCoreTicTacToe.Infrastructure.DTO
{
    public class RealPlayerDataTransferObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Side Side { get; set; }

        
    }
}
