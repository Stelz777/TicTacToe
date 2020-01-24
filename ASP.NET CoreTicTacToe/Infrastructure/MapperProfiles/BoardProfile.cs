using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles
{
    //Automapper JSON Converter ValueConverter (TypeConverter)
    

    


    public class BoardProfile : Profile
    {
        
        public BoardProfile()
        {
            CreateMap<Board, BoardDataTransferObject>()

                .ForMember(data => data.SerializedSquares, options => options.ConvertUsing(new CellListToStringConverter(), x => x.Squares))
                .ReverseMap()
                .ForMember(data => data.Squares, options => options.ConvertUsing(new StringToCellListConverter(), x => x.SerializedSquares));
        }
        
    }
}
