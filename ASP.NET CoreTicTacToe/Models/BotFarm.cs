using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class BotFarm
    {
        private List<IBot> botGroup = new List<IBot>();

        public IReadOnlyList<IBot> BotGroup => botGroup;

        public IBot Bot { get; }

        public static SimpleBot CreateSimpleBot(Game game)
        {
            SimpleBot bot = new SimpleBot(game);
            return bot;
        }

        public void AddBotToGroup(SimpleBot bot)
        {
            botGroup.Add(bot);
        }
    }
}
