using System.ComponentModel.DataAnnotations;
namespace Pedidos_API.Models.DTO
{
    public class PasswordDto
    {
        [Key]

        public int IdPass { get; set; }


        public  string? Nombre { get; set;}

        public string? Pass { get; set; }

   


    }
}
