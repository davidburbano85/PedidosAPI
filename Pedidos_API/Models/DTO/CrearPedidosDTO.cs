using System.ComponentModel.DataAnnotations;

namespace Pedidos_API.Models.DTO
{
    public class CrearPedidosDTO
    {
        [Required]
        [MaxLength(100)]
        [Key]

        public string? Nombre { get; set; }
        public int Precio { get; set; }
        public int CantidadInicial { get; set; }
        public int ConsumoTotal { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public DateTime FechadeLlegada { get; set; }


    }
}
