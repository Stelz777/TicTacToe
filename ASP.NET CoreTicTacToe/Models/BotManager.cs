using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public static class BotManager
    {
        public static void MakeFirstXMove(Game game)
        {
            if (game != null)
            {
                game.TicPlayer.Bot.MakeAutoMove(game);
            }
        }

        public static void PlayBotVsBot(Game game)
        {
            if (game != null)
            {
                while (game.CanContinue())
                {
                    game.TicPlayer.Bot.MakeAutoMove(game);
                    if (game.CanContinue())
                    {
                        game.TacPlayer.Bot.MakeAutoMove(game);
                    }
                }
            }
        }
    }
}
