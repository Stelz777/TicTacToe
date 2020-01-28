using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreTicTacToe.Infrastructure.DTO
{
    public class BoardDataTransferObject
    {
        public Guid Id { get; set; }
        
        [Required]
        public string SerializedSquares { get; set; }
    }
}
