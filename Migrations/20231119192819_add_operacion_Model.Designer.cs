﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Parcial.Migrations
{
    [DbContext(typeof(LibreriaContext))]
    [Migration("20231119192819_add_operacion_Model")]
    partial class add_operacion_Model
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("BibliotecaClientes", b =>
                {
                    b.Property<int>("LibrosId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsuariosId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LibrosId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("BibliotecaClientes");
                });

            modelBuilder.Entity("Parcial.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Antiguedad")
                        .HasColumnType("TEXT");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Parcial.Models.Libro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Año")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Multilenguaje")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Precio")
                        .HasColumnType("TEXT");

                    b.Property<int>("Stock")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Libro");
                });

            modelBuilder.Entity("Parcial.Models.Operacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Factura")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<int>("LibroId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TipoMov")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LibroId");

                    b.ToTable("Operacion");
                });

            modelBuilder.Entity("Parcial.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProviderStoreId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProviderStoreId");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("BibliotecaClientes", b =>
                {
                    b.HasOne("Parcial.Models.Libro", null)
                        .WithMany()
                        .HasForeignKey("LibrosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Parcial.Models.Cliente", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Parcial.Models.Operacion", b =>
                {
                    b.HasOne("Parcial.Models.Libro", "Libro")
                        .WithMany()
                        .HasForeignKey("LibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Libro");
                });

            modelBuilder.Entity("Parcial.Models.Store", b =>
                {
                    b.HasOne("Parcial.Models.Store", "ProviderStore")
                        .WithMany()
                        .HasForeignKey("ProviderStoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProviderStore");
                });
#pragma warning restore 612, 618
        }
    }
}
