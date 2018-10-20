using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPerfiles.Repository
{
   public interface IRepositorioWrapper : IDisposable
    {
        IRepositorioAplicacion Aplicaciones { get; }
        IRepositorioRole Roles { get; }
        IRepositorioModulo Modulos { get; }
        IRepositorioModulo Usuarios { get; }
        IRepositorioUsuarioRole UsuarioRoles { get; }
        IRepositorioUsuarioModulo UsuarioModulos { get; }
        IRepositorioRoleModuloDefault RoleModulosDefault { get; }
        Task<int> CompleteAsync();
    }
}
