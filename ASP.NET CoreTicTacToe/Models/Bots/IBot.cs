using ASP.NETCoreTicTacToe.Models.Games;

namespace ASP.NETCoreTicTacToe.Models.Bots
{
    public interface IBot
    {
        string Name { get; }
        Turn MakeAutoMove(Game game);
    }
}
