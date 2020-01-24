using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
