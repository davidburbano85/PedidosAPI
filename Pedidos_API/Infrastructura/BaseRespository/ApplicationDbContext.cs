using Microsoft.EntityFrameworkCore;
using Pedidos_API.Infrastructura.Models;
using Pedidos_API.Infrastructura.ModelsPOCO;
using System;

namespace Pedidos_API.Infrastructura.BaseRespository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Consumo> Consumo { get; set; } 
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Canciones> Canciones { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Mesas> Mesas { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<ProductosCarta> ProductosCarta { get; set; }

        //tablas con el mismo nombre q  las clases y sys propiedades CLASESS POCO
        //public DbSet<usuarios> usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            //CANCIONES
            //relacion entre   canciones  N a 1
            modelBuilder.Entity<Canciones>()
                .HasOne(e => e.Empresa);
            modelBuilder.Entity<Canciones>()
                .HasOne(s => s.Mesas);


            //CONSUMO
            //relaciones de Consumos N a 1
            modelBuilder.Entity<Consumo>()
                .HasOne(c => c.Empresa);
            modelBuilder.Entity<Consumo>()
                .HasOne(c => c.Mesas);
            modelBuilder.Entity<Consumo>()
                .HasOne(c => c.ProductosCarta);

            //MESAS
            //relacion entre  mesas N 1
            modelBuilder.Entity<Mesas>()
                .HasOne(m => m.Empresa);

            //PRODUCTOS CARTA
            //relacion entre Producos
            modelBuilder.Entity<ProductosCarta>()
                .HasOne(m => m.Empresa);
            modelBuilder.Entity<ProductosCarta>()
                .HasOne(m => m.Stock);

            //STOCK
            modelBuilder.Entity<Stock>()
                .HasOne(e => e.Empresa);

            //USUARIOS
            modelBuilder.Entity<Usuarios>()
                .HasOne(u => u.Empresa);




            

            //pra no crear un bucles
            modelBuilder.Entity<Empresa>()
                .Ignore(x => x.Mesas);
            modelBuilder.Entity<Empresa>()
                .Ignore(x => x.Canciones);
            modelBuilder.Entity<Empresa>()
                .Ignore(x => x.ProductosCarta);
            modelBuilder.Entity<Empresa>()
                .Ignore(x => x.Stocks);
            modelBuilder.Entity<Empresa>()
                .Ignore(x => x.Usuarios);
            modelBuilder.Entity<Stock>()
                .Ignore(x => x.ProductosCarta);
            modelBuilder.Entity<Mesas>()
                .Ignore(h => h.Canciones);
            modelBuilder.Entity<Empresa>()
                .Ignore(c => c.Consumos);
            modelBuilder.Entity<Mesas>()
                .Ignore(c => c.Consumo);
            modelBuilder.Entity<ProductosCarta>()
                .Ignore(c => c.Consumo);
         

        }
    }
}
