




using Microsoft.EntityFrameworkCore;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
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
        }

        #region CRUD GENERICO
        public async Task Crear(T entidad)
        {
            await dbset.AddAsync(entidad);
            await Grabar();
        }
      
        public async Task Modify(T entidad)
        {
            dbset.Update(entidad);
            await Grabar();
        }

        public async Task Remover(T entidad)
        {
            dbset.Remove(entidad);
            await Grabar();
        }
       

        public async Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
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
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbset;
            if (filtro != null)
            {
                query = query.Where(filtro);
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
