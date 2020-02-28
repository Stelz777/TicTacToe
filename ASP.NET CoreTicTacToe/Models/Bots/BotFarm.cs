using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Models.Bots
{
    public class BotFarm
    {
        private List<IBot> botPool = new List<IBot>();
        
        public IReadOnlyList<IBot> BotGroup => botPool;
        
        public void AddBotToPool(IBot bot)
        {
            botPool.Add(bot);
        }
    }
}
