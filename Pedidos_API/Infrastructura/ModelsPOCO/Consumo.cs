using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Pedidos_API.Infrastructura.ModelsPOCO
{
    public class Consumo
    {
        [Key]

        public int Id { get; set; }
        public string? Mesa{ get; set; }
        [Required]
        public string Producto { get; set; }

        public int Cantidad { get; set; }



    }
}
