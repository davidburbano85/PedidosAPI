using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pedidos_API.Infrastructura.Models;

namespace Pedidos_API.Infrastructura.ModelsPOCO
{
    public class Consumo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [ForeignKey("Empresa")]
        public int idEmpresa{ get; set; }
        [Required]
        [ForeignKey("Mesas")]

        public int idMesa{ get; set; }
        [Required]
        [ForeignKey("ProductosCarta")]

        public int idProducto { get; set; }

        public int cantidad { get; set; }
        public bool atencionMesero { get; set; }
        public DateTime fechaDeVenta { get; set; }


        //un consumo para :
        public Empresa Empresa { get; set; }
        public Mesas Mesas { get; set; }
        public virtual ProductosCarta ProductosCarta { get; set; }





    }
}
