using ApiPerfiles.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiPerfiles.Repository
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected readonly DbContext Context;
        private DbSet<T> _entities;

        public RepositorioBase(DbContext context)
        {
            this.Context = context;
            this._entities = Context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await this._entities.AddAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await this._entities.AddRangeAsync(entities);

            return entities;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicado)
        {
            return await _entities.Where(predicado).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public void Remove(T entity)
        {
            this._entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            this._entities.RemoveRange(entities);
        }

        public T Update(T entity)
        {
            this._entities.Update(entity);


            return entity;
        }
    }

    public class RepositorioAplicacion : RepositorioBase<Aplicacion>, IRepositorioAplicacion
    {
        public RepositorioAplicacion(PerfilDbContext contexto) : base(contexto)
        {
        }

        public PerfilDbContext appDbContext
        {
            get { return Context as PerfilDbContext; }
        }
    }


    public class RepositorioRole : RepositorioBase<Role>, IRepositorioRole
    {
        public RepositorioRole(PerfilDbContext contexto) : base(contexto)
        {
        }

        public PerfilDbContext appDbContext
        {
            get { return Context as PerfilDbContext; }
        }
    }


    public class RepositorioModulo : RepositorioBase<Modulo>, IRepositorioModulo
    {
        public RepositorioModulo(PerfilDbContext contexto) : base(contexto)
        {
        }

        public PerfilDbContext appDbContext
        {
            get { return Context as PerfilDbContext; }
        }
    }


    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(PerfilDbContext contexto) : base(contexto)
        {
        }

        public PerfilDbContext appDbContext
        {
            get { return Context as PerfilDbContext; }
        }

      
    }


    public class RepositorioUsuarioRole : RepositorioBase<UsuarioRole>, IRepositorioUsuarioRole
    {
        public RepositorioUsuarioRole(PerfilDbContext contexto) : base(contexto)
        {
        }

        public PerfilDbContext appDbContext
        {
            get { return Context as PerfilDbContext; }
        }

        public IEnumerable<UsuarioRole> GetUsuarioRoleConRoles(int id)
        {
            return appDbContext.UsuarioRoles.Where(x => x.UsuarioId == id).Include(x => x.Role)
                   .DefaultIfEmpty(new UsuarioRole())
                   .ToList();
        }
    }


    public class RepositorioUsuarioModulo : RepositorioBase<UsuarioModulo>, IRepositorioUsuarioModulo
    {
        public RepositorioUsuarioModulo(PerfilDbContext contexto) : base(contexto)
        {
        }

        public PerfilDbContext appDbContext
        {
            get { return Context as PerfilDbContext; }
        }
    }

    public class RepositorioRoleModuloDefault : RepositorioBase<RoleModuloDefault>, IRepositorioRoleModuloDefault
    {
        public RepositorioRoleModuloDefault(PerfilDbContext contexto) : base(contexto)
        {
        }

        public PerfilDbContext appDbContext
        {
            get { return Context as PerfilDbContext; }
        }
    }


}
