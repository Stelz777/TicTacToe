﻿using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Models
{
    public class BotFarm
    {
        private List<IBot> botPool = new List<IBot>();
        private static readonly BotFarm instance = new BotFarm();

        public IReadOnlyList<IBot> BotGroup => botPool;
        public IBot Bot { get; }
        public static BotFarm Current => instance;

        public static SimpleBot CreateSimpleBot(Game game)
        {
            SimpleBot bot = new SimpleBot(Side.Tac);
            return bot;
        }

        public void AddBotToPool(SimpleBot bot)
        {
            botPool.Add(bot);
        }
    }
}
