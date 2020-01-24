using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
