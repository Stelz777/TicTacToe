using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class History
    {
        //TODO later
        //Хранит turns, восстанавливает board
        //Добавить свойство для получения последнего хода

        private List<Board> boards = new List<Board>();

        public List<Board> Boards
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

        public History()
        {
            boards.Add(new Board());
        }
    }
}
