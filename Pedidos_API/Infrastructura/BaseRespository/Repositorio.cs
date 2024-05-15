




using Microsoft.EntityFrameworkCore;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using System;
using System.Linq.Expressions;

namespace Pedidos_API.Infrastructura.BaseRespository

{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            dbset = _db.Set<T>();
            //var loquesea = Activator.CreateInstance(typeof(T));
        }

        #region CRUD GENERICO
        public async Task Crear(T entidad, params Expression<Func<T, object>>[] includes)
        {
            await dbset.AddAsync(entidad);
            await Grabar();
        }

        public async Task Modify(T entidad, params Expression<Func<T, object>>[] includes)
        {
            dbset.Update(entidad);
            await Grabar();
        }

        public async Task Remover(T entidad, params Expression<Func<T, object>>[] includes)
        {
            dbset.Remove(entidad);
            await Grabar();
        }
       

        public async Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbset;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (includes.Any())
            {
                foreach (var navProperty in includes)
                    query = query.Include(navProperty);

            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbset;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (includes.Any())
            {
                foreach (var navProperty in includes)
                    query = query.Include(navProperty);

            }
            return await query.ToListAsync();
        }
      
        #endregion


        public async Task Grabar()
        {
            await _db.SaveChangesAsync();

        }

    }
}
