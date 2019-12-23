using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class History
    {
        private List<Board> turns;

        public List<Board> Turns
        {
            get
            {
                return turns;
            }
            set
            {
                turns = value;
            }
        }
    }
}
