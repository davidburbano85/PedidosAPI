using System.ComponentModel.DataAnnotations;
namespace Pedidos_API.Models.DTO
{
    public class CancionesDto
    {
        public int idCancion { get; set; }
        public string? linkCopiado { get; set; }
        [Required]
        public string? linkFiltrado { get; set; }

    }
}
