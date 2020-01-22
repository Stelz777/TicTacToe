using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameDataTransferObject>()
                .ReverseMap();
        }
    }
}
