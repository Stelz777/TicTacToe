using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;

namespace ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            CreateMap<History, HistoryDataTransferObject>()
                .ReverseMap()
                .ForMember(destination => destination.Turns, options => options.MapFrom(options => options.Turns));
        }   
    }
}
