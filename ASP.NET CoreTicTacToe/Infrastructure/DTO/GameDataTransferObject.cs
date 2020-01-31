using ASP.NETCoreTicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Infrastructure.DTO
{
    public class GameDataTransferObject
    {
        public int ID { get; set; }
        public HistoryDataTransferObject History { get; set; }
        public BoardDataTransferObject Board { get; set; }

        public PlayerDataTransferObject TicPlayer { get; set; }

        public PlayerDataTransferObject TacPlayer { get; set; }
    }
}
