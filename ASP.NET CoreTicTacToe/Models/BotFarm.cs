using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class BotFarm
    {
        private List<IBot> botPool = new List<IBot>();

        public IReadOnlyList<IBot> BotGroup => botPool;

        public IBot Bot { get; }

        private static readonly BotFarm instance = new BotFarm();
        public static BotFarm Current => instance;

        public static SimpleBot CreateSimpleBot(Game game)
        {
            SimpleBot bot = new SimpleBot(game, Side.Tac);
            return bot;
        }

        public void AddBotToPool(SimpleBot bot)
        {
            botPool.Add(bot);
        }
    }
}
