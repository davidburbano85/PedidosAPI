using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.ModelsPOCO;

namespace Pedidos_API.Infrastructura.Repositorios
{
    public class ConsumoRepositorio : Repositorio<Consumo>, IConsumoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ConsumoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
