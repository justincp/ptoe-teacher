using Microsoft.EntityFrameworkCore;

namespace PTOEQuiz.Data
{
    public class QuizContext : DbContext
    {
        public QuizContext(
            DbContextOptions options) : base(options)
        { }

        public DbSet<QuizResult> QuizResults { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}