using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.ModelsPOCO;

namespace Pedidos_API.Infrastructura.Repositorios
{
    public class CancionesRepositorio : Repositorio<Canciones>, ICancionesRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CancionesRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int sumar(int a, int b)
        {
            return a + b;
        }
    }
}
