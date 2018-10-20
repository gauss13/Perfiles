using ApiPerfiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiPerfiles.Repository
{
   public interface IRepositorioBase<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicado);

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        T Update(T entity);

    }

    public interface IRepositorioAplicacion : IRepositorioBase<Aplicacion>
    {
    }

    public interface IRepositorioRole : IRepositorioBase<Role>
    {
    }

    public interface IRepositorioModulo : IRepositorioBase<Modulo>
    {
    }

    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
    }

    public interface IRepositorioUsuarioRole : IRepositorioBase<UsuarioRole>
    {
    }


    public interface IRepositorioUsuarioModulo : IRepositorioBase<UsuarioModulo>
    {
    }

    public interface IRepositorioRoleModuloDefault : IRepositorioBase<RoleModuloDefault>
    {
    }

}
