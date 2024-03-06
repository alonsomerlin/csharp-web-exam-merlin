using csharp_web_exam_api.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_web_exam_api.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<ALUMNO> ALUMNOs { get; set; }
        public DbSet<GENERO> GENEROs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ALUMNO>().HasIndex(c => c.ALUMNO_ID).IsUnique();
            modelBuilder.Entity<GENERO>().HasIndex(c => c.GENERO_ID).IsUnique();

        }
    }
}
