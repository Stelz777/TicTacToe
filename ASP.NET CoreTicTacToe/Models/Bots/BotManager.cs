using ASP.NETCoreTicTacToe.Models.Games;

namespace ASP.NETCoreTicTacToe.Models.Bots
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

        private static void MakeBotTurn(Player player, Game game)
        {
            if (player.Bot != null)
            {
                player.Bot.MakeAutoMove(game);
            }
        }
    }
}
