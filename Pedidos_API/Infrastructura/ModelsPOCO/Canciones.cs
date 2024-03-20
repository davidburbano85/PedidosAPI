using System.ComponentModel.DataAnnotations;

namespace Pedidos_API.Infrastructura.ModelsPOCO
{
    public class Canciones
    {
        [Key]

        public int idCancion { get; set; }
       
        public string? linkcopiado { get; set; }
        [Required]
        public string? linkfiltrado { get; set; }

        

      
    }
}
