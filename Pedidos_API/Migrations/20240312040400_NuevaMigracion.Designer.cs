﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pedidos_API.Datos_Bebidas;

#nullable disable

namespace Pedidos_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240312040400_NuevaMigracion")]
    partial class NuevaMigracion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pedidos_API.Models.Pedidoss", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechadeLlegada")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Pedidos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cantidad = 10,
                            FechadeLlegada = new DateTime(2024, 3, 11, 23, 4, 0, 236, DateTimeKind.Local).AddTicks(5751),
                            Nombre = "Tequila",
                            Precio = 50000
                        },
                        new
                        {
                            Id = 2,
                            Cantidad = 70,
                            FechadeLlegada = new DateTime(2024, 3, 11, 23, 4, 0, 236, DateTimeKind.Local).AddTicks(5774),
                            Nombre = "Cerveza",
                            Precio = 4000
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
