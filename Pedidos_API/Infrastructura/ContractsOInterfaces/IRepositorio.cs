﻿using System.Linq.Expressions;

namespace Pedidos_API.Infrastructura.ContractsOInterfaces
{
    public interface IRepositorio<T> where T : class
    {
        Task Crear(T entidad, params Expression<Func<T, object>>[] includes);
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, params Expression<Func<T, object>>[]includes);
        Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true, params Expression<Func<T, object>>[]includes);
        Task Modify(T entidad, params Expression<Func<T, object>>[] includes);
        Task Remover(T entidad, params Expression<Func<T, object>>[] includes);
    }
    
}
