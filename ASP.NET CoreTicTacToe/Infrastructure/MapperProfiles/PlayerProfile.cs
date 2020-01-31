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
                .ReverseMap();
        }   
    }
}
