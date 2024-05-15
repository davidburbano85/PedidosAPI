using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Models.DTO
{
    public class CrearConsumoDTO
    {

        [Key]

        public int id { get; set; }
        [Required]
        [ForeignKey("Empresa")]
        public int idEmpresa { get; set; }
        [Required]
        [ForeignKey("Mesas")]

        public int idMesa { get; set; }
        [Required]
        [ForeignKey("ProctosCarta")]

        public int idProducto { get; set; }

        public int cantidad { get; set; }
        public bool atencionMesero { get; set; }
        public DateTime fechaDeVenta { get; set; }

    }
}
