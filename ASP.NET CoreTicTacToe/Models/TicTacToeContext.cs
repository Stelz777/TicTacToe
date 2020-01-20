using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class TicTacToeContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        /*public DbSet<History> Histories { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Turn> Turns { get; set; }*/

        public TicTacToeContext(DbContextOptions<TicTacToeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;");
        }
    }
}
