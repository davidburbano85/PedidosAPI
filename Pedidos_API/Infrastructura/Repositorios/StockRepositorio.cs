using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.Models;

namespace Pedidos_API.Infrastructura.Repositorios
{
    public class StockRepositorio : Repositorio<Stock>, IStockRepositorio
    {
        private readonly ApplicationDbContext _db;

        public StockRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
