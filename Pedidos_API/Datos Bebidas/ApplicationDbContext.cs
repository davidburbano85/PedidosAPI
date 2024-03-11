using Microsoft.EntityFrameworkCore;
using Pedidos_API.Models;

namespace Pedidos_API.Datos_Bebidas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
                
        }
        public DbSet<Pedidoss> Pedidos{get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedidoss>().HasData(
                new Pedidoss()
                {
                    Id=1,
                    Nombre="Tequila",
                    Cantidad=10,
                    Precio=50000,
                    FechadeLlegada = DateTime.Now.AddYears(0)

                },
                  new Pedidoss()
                  {
                      Id = 2,
                      Nombre = "Cerveza",
                      Cantidad = 70,
                      Precio = 4000,
                      FechadeLlegada = DateTime.Now.AddYears(0)

                  }
                );
        }
    }
}
