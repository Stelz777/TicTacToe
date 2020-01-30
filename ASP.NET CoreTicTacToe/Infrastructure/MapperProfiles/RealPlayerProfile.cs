using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;

namespace ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles
{
    public class RealPlayerProfile : Profile
    {
        public RealPlayerProfile()
        {
            CreateMap<RealPlayer, RealPlayerDataTransferObject>()
                .ReverseMap();
        }   
    }
}
