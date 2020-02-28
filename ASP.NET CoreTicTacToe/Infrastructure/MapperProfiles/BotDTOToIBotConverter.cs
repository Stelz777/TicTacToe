using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using ASP.NETCoreTicTacToe.Models.Bots;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles
{
    public class BotDTOToIBotConverter : IValueConverter<PlayerDataTransferObject, IBot>
    {
        public IBot Convert(PlayerDataTransferObject source, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }
            var botTypes = (from type in Assembly.GetExecutingAssembly().GetExportedTypes()
                            where !type.IsInterface && !type.IsAbstract
                            where typeof(IBot).IsAssignableFrom(type)
                            select type).ToArray();
            var instantiatedBots =
                botTypes.Select(type => (IBot)Activator.CreateInstance(type, source.Side)).ToArray();
            foreach (var bot in instantiatedBots)
            {
                if (bot.Name == source.Difficulty)
                {
                    return bot;
                }
            }
            return null;
        }
    }
}
