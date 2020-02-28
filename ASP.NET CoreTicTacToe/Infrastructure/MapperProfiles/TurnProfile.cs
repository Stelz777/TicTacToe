using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;

namespace ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles
{
    public class TurnProfile : Profile
    {
        public TurnProfile()
        {
            CreateMap<Turn, TurnDataTransferObject>()
                .ReverseMap();
        }
    }
}
