using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.ModelsPOCO;

namespace Pedidos_API.Infrastructura.Repositorios
{
    public class PasswordRepositorio : Repositorio<Password>, IPasswordRepositorio
    {
        private readonly ApplicationDbContext _db;

        public PasswordRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
