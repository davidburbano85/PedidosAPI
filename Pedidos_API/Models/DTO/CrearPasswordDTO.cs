using System.ComponentModel.DataAnnotations;

namespace Pedidos_API.Models.DTO
{
    public class CrearPasswordDTO
    {
        [Key]

        public int IdPass { get; set; }

        public string Nombre { get; set; }

        public string? pass { get; set; }

     

        
    }
}
