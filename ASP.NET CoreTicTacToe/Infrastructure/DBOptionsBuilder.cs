using ASP.NETCoreTicTacToe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Infrastructure
{
    public class DBOptionsBuilder
    {
        public static DbContextOptionsBuilder<TicTacToeContext> Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder;
        }
    }
}
