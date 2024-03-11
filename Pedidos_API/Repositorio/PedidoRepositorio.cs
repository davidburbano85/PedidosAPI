using Pedidos_API.Datos_Bebidas;
using Pedidos_API.Models;
using Pedidos_API.Repositorio.IRepositorio;

namespace Pedidos_API.Repositorio
{
    public class PedidoRepositorio : Repositorio<Pedidoss>, IPedidoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public PedidoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Pedidoss> Actualizar(Pedidoss entidad)
        {
            entidad.FechadeLlegada=DateTime.Now;
            _db.Pedidos.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
