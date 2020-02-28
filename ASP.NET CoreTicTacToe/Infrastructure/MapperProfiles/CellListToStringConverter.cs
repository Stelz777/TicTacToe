using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles
{
    public class CellListToStringConverter : IValueConverter<IReadOnlyList<Cell>, string>
    {
        public string Convert(IReadOnlyList<Cell> source, ResolutionContext context)
        {
            return string.Join(',', source);
        }
    }
}
