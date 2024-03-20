using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Pedidos_API.Infrastructura.Models
{

    public class Pedidos

    {
        [Key]

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Precio { get; set; }
        public  int CantidadInicial { get; set; }
        public int ConsumoTotal { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public DateTime FechadeLlegada { get; set; }


        

    }

}
