using Microsoft.EntityFrameworkCore;
using Pedidos_API.Infrastructura.Models;

namespace Pedidos_API.Infrastructura.BaseRespository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Pedidos> Pedidos { get; set; }
        //tablas con el mismo nombre q  las clases y sys propiedades CLASESS POCO
        //public DbSet<usuarios> usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
