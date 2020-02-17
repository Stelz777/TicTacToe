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
                .ForMember(data => data.Difficulty, options => options.MapFrom(map => map.Bot is SimpleBot ? "Simple" : null))
                .ReverseMap()
                .ForPath(data => data.Bot, options => options.MapFrom(map => map.IsBot ? map.Difficulty == "Simple" ? new SimpleBot(map.Side) : null : null));
        }   
    }
}
