using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.Models;

namespace Pedidos_API.Infrastructura.Repositorios
{
    public class MesasRepositorio : Repositorio<Mesas>, IMesasRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MesasRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
