using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Models.DTO
{
    public class ProductosCartaDto
    {
        [Key]

        public int id { get; set; }
        [Required]
        [ForeignKey("Empresa")]
        public int idEmpresa { get; set; }
        [Required]
        [ForeignKey("Stock")]
        public int idStock { get; set; }
        public string nombreProducto { get; set; }
        public string tipoProducto { get; set; }
        public string unidad { get; set; }


    }
}
