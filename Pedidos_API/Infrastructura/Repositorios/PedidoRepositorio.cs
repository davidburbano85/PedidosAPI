using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.Models;

namespace Pedidos_API.Infrastructura.Repositorios
{
    public class PedidoRepositorio : Repositorio<Pedidos>, IPedidoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public PedidoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
