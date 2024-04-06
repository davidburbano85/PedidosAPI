using Pedidos_API.Infrastructura.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Models.DTO
{
    public class StockDto
    {
        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey("Empresa")]
        public int idEmpresa { get; set; }
        public string tipoProducto { get; set; }

        public int disponibilidad { get; set; }
        public long precioCompra { get; set; }
        public  long precioVenta { get; set; }
        public DateTime fechaCompra { get; set; }
        public string Proveedor { get; set; }

        public virtual Empresa Empresa { get; set; }

    }
}
