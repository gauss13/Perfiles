using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPerfiles.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPerfiles.Models
{
    public class PerfilDbContext : DbContext
    {
        public PerfilDbContext(DbContextOptions<PerfilDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioRole>().HasKey(x => new { x.UsuarioId, x.RoleId });
            modelBuilder.Entity<UsuarioModulo>().HasKey(x => new { x.UsuarioId, x.ModuloId});
        }

        // DbSet
        public DbSet<Aplicacion> Aplicaciones { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRole> UsuarioRoles { get; set; }
        public DbSet<UsuarioModulo> UsuarioModulos { get; set; }
        public DbSet<RoleModuloDefault> RoleModulosDefault { get; set; }


    }
}
