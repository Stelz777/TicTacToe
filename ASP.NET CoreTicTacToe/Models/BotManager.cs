using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class BotManager
    {
        public static void MakeFirstXMove(Game game)
        {
            if (game != null)
            {
                game.TicPlayer.Bot.MakeAutoMove(game);
            }
        }

        public void PlayBotVsBot(Game game)
        {
            if (game != null && game.CanContinue())
            {
                if (game.History.LastTurn == null)
                {
                    MakeBotTurn(game.TicPlayer, game);
                }
                else
                {
                    if (game.History.LastTurn.Side == Side.Tic)
                    {
                        MakeBotTurn(game.TacPlayer, game);
                    }
                    else if (game.History.LastTurn.Side == Side.Tac)
                    {
                        MakeBotTurn(game.TicPlayer, game);
                    }
                }
            }
        }

        private void MakeBotTurn(Player player, Game game)
        {
            player.Bot.MakeAutoMove(game);
        }
    }
}
