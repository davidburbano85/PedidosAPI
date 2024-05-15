using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.ModelsPOCO;

namespace Pedidos_API.Infrastructura.Repositorios
{
    public class UsuariosRepositorio : Repositorio<Usuarios>, IUsuariosRepositorio
    {
        private readonly ApplicationDbContext _db;

        public UsuariosRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
