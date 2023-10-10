using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Parcial.Models;

    public class LibreriaContext : DbContext
    {
        public LibreriaContext (DbContextOptions<LibreriaContext> options)
            : base(options)
        {
        }

        public DbSet<Parcial.Models.Libro> Libro { get; set; } = default!;

        public DbSet<Parcial.Models.Cliente> Cliente { get; set; } = default!;
    }
