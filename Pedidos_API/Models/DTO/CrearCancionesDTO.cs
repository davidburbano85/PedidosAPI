using System.ComponentModel.DataAnnotations;

namespace Pedidos_API.Models.DTO
{
    public class CrearCancionesDTO
    {
       

        public int idCancion { get; set; }

        public string? linkcopiado { get; set; }
        [Required]
        public string? linkfiltrado { get; set; }


    }
}
