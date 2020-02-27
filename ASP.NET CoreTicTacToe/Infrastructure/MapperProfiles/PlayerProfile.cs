using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;

namespace ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerDataTransferObject>()
                .ForMember(data => data.IsBot, options => options.MapFrom(map => map.Bot != null ? true : false))
                .ForMember(data => data.Difficulty, options => options.MapFrom(
                    map => map.Bot is SimpleBot ? "Simple" : map.Bot is MinimaxBot ? "Minimax" : null))
                .ReverseMap()
                .ForPath(data => data.Bot, options => options.MapFrom(
                    map => map.IsBot ? map.Difficulty == "Simple" ? (IBot) new SimpleBot(map.Side)
                                                                  : map.Difficulty == "Minimax" ? (IBot) new MinimaxBot(map.Side) 
                                                                                                : null
                                     : null));
        }   
    }
}
