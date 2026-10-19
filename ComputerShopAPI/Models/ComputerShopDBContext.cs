using Microsoft.EntityFrameworkCore;

namespace ComputerShopAPI.Models
{
    public class ComputerShopDBContext : DbContext
    {
        public ComputerShopDBContext(DbContextOptions options) : base(options)
        {
        }

        public ComputerShopDBContext()
        {

        }
        
        public DbSet<Osystem> Osystems { get; set; }
        public DbSet<Computer> Computers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = "server=localhost;database=computershop;uid=root;password=password";

            optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));
        }

    }
}
