using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Infrastructura.Models
{

    public class Stock

    {
        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey("Empresa")]
        public int idEmpresa { get; set; }
        public string tipoProducto { get; set; }

        public int disponibilidad { get; set; }
        public long precioCompra { get; set; }
        public long precioVenta { get; set; }
        public DateTime fechaCompra { get; set; }
        public string Proveedor { get; set; }  

        //crear relaciones foraneas
        public virtual Empresa Empresa { get; set; }
        public virtual List<ProductosCarta> ProductosCarta { get; set;}


     


        

    }

}
