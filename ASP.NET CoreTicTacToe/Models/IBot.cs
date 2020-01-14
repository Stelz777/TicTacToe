using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public interface IBot
    {
        Turn MakeAutoMove(Board board);
    }
}
