using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.Models;

namespace Pedidos_API.Infrastructura.Repositorios
{
    public class EmpresaRepositorio : Repositorio<Empresa>, IEmpresaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public EmpresaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
