using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using ASP.NETCoreTicTacToe.Models;
using ASP.NETCoreTicTacToe.Models.Bots;
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
                    map => map.Bot != null ? map.Bot.Name : null))
                .ReverseMap()
                .ForMember(data => data.Bot, options => options.ConvertUsing(
                    new BotDTOToIBotConverter(), playerDTO => playerDTO));
        }   
    }
}
