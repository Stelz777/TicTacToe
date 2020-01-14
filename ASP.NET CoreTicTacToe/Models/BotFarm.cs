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

        public Bot CreateSimpleBot(Game game)
        {
            Bot bot = new Bot(game);
            return bot;
        }

        public void AddBotToGroup(Bot bot)
        {
            botGroup.Add(bot);
        }
    }
}
