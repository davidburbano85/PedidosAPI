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
            //MESAS
            //relacion entre Empresa mesas 1 a N
            modelBuilder.Entity<Mesas>()
             .HasOne(m => m.Empresa)
            .WithMany(p => p.Mesas)
            .HasForeignKey(m => m.idEmpresa);


            //CANCIONES
            //relacion entre  mesas canciones 1 a N
            modelBuilder.Entity<Canciones>()
           .HasOne(s => s.Mesas);
            //pra no crear un bucle empresas mesas
            modelBuilder.Entity<Empresa>()
                .Ignore(x => x.Mesas);

            // // relacion entre empresa canciones 1 a N
            modelBuilder.Entity<Canciones>()
           .HasOne(s => s.Empresa);

            //CONSUMOS
            //relacion entre EMPRESA, MESAS PRODUCTOS CARTA y CONSUMOS
            modelBuilder.Entity<Consumo>()
                .HasOne(s => s.Empresa);
            
            modelBuilder.Entity<Consumo>()
                .HasOne(s => s.Mesas);
            modelBuilder.Entity<Consumo>()
                .HasOne(s => s.ProductosCarta);

            //PRODUCTOS CARTA
            //relacion entre EMPRESA STOCKY PRODUCTOS CARTA
            modelBuilder.Entity<ProductosCarta>()
                .HasOne(p => p.Empresa);
            modelBuilder.Entity<ProductosCarta>()
                .HasOne(p => p.Stock);

            //STOCK
            // relacion entre EMPRESA Y STOCK
            modelBuilder.Entity<Stock>()
                            .HasOne(p => p.Empresa);
            
                       
            
            //USUARIOS
            //relacion empresa usuario
            modelBuilder.Entity<Usuarios>()
            .HasOne(f => f.Empresa);

            
            
            



        }
    }
}
