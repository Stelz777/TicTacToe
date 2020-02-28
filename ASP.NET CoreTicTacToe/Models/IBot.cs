using ASP.NETCoreTicTacToe.Models;

namespace ASP.NETCoreTicTacToe.Models
{
    public interface IBot
    {
        string Name { get; }
        Turn MakeAutoMove(Game game);
    }
}
