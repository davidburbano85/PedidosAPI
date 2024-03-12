﻿




using Microsoft.EntityFrameworkCore;
using Pedidos_API.Datos_Bebidas;
using Pedidos_API.Repositorio.IRepositorio;
using System.Linq.Expressions;

namespace Pedidos_API.Repositorio
   
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbset= _db.Set<T>();
        }
        public async Task Crear(T entidad)
        {
            await dbset.AddAsync(entidad);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();

        }

        public async Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filtro != null)
            {
                query=query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public  async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbset;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query.ToListAsync();
        }

        public async Task Remove(T entidad)
        {
            dbset.Remove(entidad);
            await Grabar();
        }
    }
}
