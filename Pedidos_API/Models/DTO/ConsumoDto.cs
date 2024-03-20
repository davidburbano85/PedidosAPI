using System.ComponentModel.DataAnnotations;
namespace Pedidos_API.Models.DTO
{
    public class ConsumoDto
    {
        public int Id { get; set; }
        //[Required]
        //[MaxLength(100)]
        //[Key]
               
        public string? Mesa { get; set; }
        [Required]
        public string Producto { get; set; }

        public int Cantidad { get; set; }


    }
}
