using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class TicTacToeContext : DbContext
    {
        public DbSet<GameDataTransferObject> Games { get; set; }
        public DbSet<HistoryDataTransferObject> Histories { get; set; }
        public DbSet<BoardDataTransferObject> Boards { get; set; }
        public DbSet<TurnDataTransferObject> Turns { get; set; }
        
        public TicTacToeContext(DbContextOptions<TicTacToeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardDataTransferObject>().Property(x => x.SerializedSquares).IsRequired();
        }
    }
}
