using Microsoft.EntityFrameworkCore;
using JwtProjeto.Models.Models;
namespace JwtProjeto.Database.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ProductModel> Products { get; set; }
    }
}