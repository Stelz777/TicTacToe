using ASP.NET_CoreTicTacToe.Models;

namespace ASP.NETCoreTicTacToe.Models
{
    public interface IBot
    {
        Turn MakeAutoMove(GameAPI gameAPI);
    }
}
