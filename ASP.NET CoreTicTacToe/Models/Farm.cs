using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Farm
    {
        private Dictionary<int, Board> boards = new Dictionary<int, Board>();
        private int currentGame = 0;

        public Dictionary<int, Board> Boards
        {
            get
            {
                return boards;
            }
            set
            {
                boards = value;
            }
        }

        public (int, Board) FindBoard(int? id) {
            if (!id.HasValue || !boards.TryGetValue(out var foundBoard)) {
                return (id ?? boards.Keys.Max()+1, new Board());
            }
            
            return (id.Value, foundBoard);
        }
    }
}
