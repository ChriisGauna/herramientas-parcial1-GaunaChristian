
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parcial.Models;

    public class LibreriaContext : IdentityDbContext
    {
        public LibreriaContext (DbContextOptions<LibreriaContext> options)
            : base(options)
        {
        }

        public DbSet<Parcial.Models.Libro> Libro { get; set; } = default!;

        public DbSet<Parcial.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<Parcial.Models.Store> Store { get; set; } = default!;
        public DbSet<Parcial.Models.Operacion> Operacion { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>()
            .HasMany(e => e.Usuarios)
            .WithMany(e => e.Libros)
            .UsingEntity("BibliotecaClientes");
            base.OnModelCreating(modelBuilder);
        }
    }
