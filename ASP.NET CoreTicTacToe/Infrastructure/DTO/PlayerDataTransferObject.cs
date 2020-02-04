using ASP.NETCoreTicTacToe.Models;
using System;

namespace ASP.NETCoreTicTacToe.Infrastructure.DTO
{
    public class PlayerDataTransferObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Side Side { get; set; }
        public bool IsBot { get; set; }
        public string Difficulty { get; set; }
    }
}
