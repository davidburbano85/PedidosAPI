using System.Linq.Expressions;

namespace Pedidos_API.Infrastructura.ContractsOInterfaces
{
    public interface IRepositorio<T> where T : class
    {
        Task Crear(T entidad);
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null);
        Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true);
        Task Modify(T entidad);
        Task Remover(T entidad);
    }
    
}
