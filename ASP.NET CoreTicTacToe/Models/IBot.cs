using ASP.NETCoreTicTacToe.Models;

namespace ASP.NETCoreTicTacToe.Models
{
    public interface IBot
    {
        Turn MakeAutoMove();
    }
}
