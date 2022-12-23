using Microsoft.EntityFrameworkCore;

namespace PTOEQuiz
{
      public class GameContext : DbContext
      {
            public GameContext(
                DbContextOptions options) : base(options)
            {
            }
    
            public DbSet<GameResult> GameResults { get; set; }
    
           
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
    
            }
      }
}