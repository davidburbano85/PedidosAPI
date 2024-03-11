using Pedidos_API.Models;

namespace Pedidos_API.Repositorio.IRepositorio
{
    public interface IPedidoRepositorio:IRepositorio<Pedidoss>
    {
        Task<Pedidoss> Actualizar(Pedidoss entidad);

    }
}
