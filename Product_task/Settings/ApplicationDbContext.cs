using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Settings
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        public DbSet<Items> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
