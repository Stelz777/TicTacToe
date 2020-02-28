using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreTicTacToe.Infrastructure
{
    public class TicTacToeContext : DbContext
    {
        public DbSet<GameDataTransferObject> Games { get; set; }
        public DbSet<HistoryDataTransferObject> Histories { get; set; }
        public DbSet<BoardDataTransferObject> Boards { get; set; }
        public DbSet<TurnDataTransferObject> Turns { get; set; }

        public DbSet<PlayerDataTransferObject> Players { get; set; }

        public DbSet<UserDataTransferObject> Users { get; set; }
        
        public TicTacToeContext(DbContextOptions<TicTacToeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Entity<BoardDataTransferObject>().Property(x => x.SerializedSquares).IsRequired();
            }
        }
    }
}
