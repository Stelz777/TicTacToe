using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class GameDataTransferObject
    {
        public int ID { get; set; }
        public History History { get; set; }
        public Board Board { get; set; }
    }
}
