using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.Models;

namespace Pedidos_API.Infrastructura.Repositorios
{
    public class ProductosCartaRepositorio : Repositorio<ProductosCarta>, IProductosCartaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProductosCartaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
