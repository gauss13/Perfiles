using ApiPerfiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPerfiles.Repository
{
    public class RepositorioWrapper : IRepositorioWrapper
    {
        PerfilDbContext _Context { get; }

        public RepositorioWrapper(PerfilDbContext context)
        {
            _Context = context;

            Aplicaciones = new RepositorioAplicacion(_Context);
            Roles = new RepositorioRole(_Context);
            Modulos = new RepositorioModulo(_Context);
            Usuarios = new RepositorioModulo(_Context);
            UsuarioRoles = new RepositorioUsuarioRole(_Context);
            UsuarioModulos = new RepositorioUsuarioModulo(_Context);
            RoleModulosDefault = new RepositorioRoleModuloDefault(_Context);
        }

        public IRepositorioAplicacion Aplicaciones { get; private set; }
        public IRepositorioRole Roles { get; private set; }
        public IRepositorioModulo Modulos { get; private set; }
        public IRepositorioModulo Usuarios { get; private set; }
        public IRepositorioUsuarioRole UsuarioRoles { get; private set; }
        public IRepositorioUsuarioModulo UsuarioModulos { get; private set; }
        public IRepositorioRoleModuloDefault RoleModulosDefault { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await this._Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            // liberar los recuros de memoria
            this._Context.Dispose();
        }

    }
}
