using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Farm
    {
        private Dictionary<int, Board> boards = new Dictionary<int, Board>();

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
            if (!id.HasValue || !boards.TryGetValue(id.Value, out Board foundBoard)) 
            {
                var newBoard = new Board();
                
                int newId;
                if (id == null)
                {
                    if (boards.Count > 0)
                    {
                        newId = boards.Keys.Max() + 1;
                        
                    }
                    else
                    {
                        newId = 0;
                    }
                }
                else
                {
                    newId = id.Value;
                    
                }
                boards.Add(newId, newBoard);
                return (newId, newBoard);
            }
            
            return (id.Value, foundBoard);
        }
    }
}
