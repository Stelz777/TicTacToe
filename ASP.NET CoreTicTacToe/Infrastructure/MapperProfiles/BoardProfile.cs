using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;

namespace ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<Board, BoardDataTransferObject>()
                .ForMember(data => data.SerializedSquares, options => options.ConvertUsing(new CellListToStringConverter(), board => board.Squares))
                .ReverseMap()
                .ForMember(data => data.Squares, options => options.ConvertUsing(new StringToCellListConverter(), boardDTO => boardDTO.SerializedSquares));
        }
    }
}
