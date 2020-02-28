using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles
{
    public class StringToCellListConverter : IValueConverter<string, List<Cell>>
    {
        public List<Cell> Convert(string source, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }
            var strings = source.Split(',').ToList();
            return strings.Select(s => (Cell)Enum.Parse(typeof(Cell), s)).ToList();  
        }
    }
}
