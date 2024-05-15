using Pedidos_API.Infrastructura.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Models.DTO
{
    public class UsuariosDto
    {


        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey("Empresa")]
        public int idEmpresa { get; set; }

        public string? contraseña { get; set; }
        public string nombre { get; set; }
        public string tipoUsuario { get; set; }

        public virtual Empresa Empresa { get; set; }
        public string enviarPass()
        {
            return Encript.GetSHA256(contraseña);
        }




    }
}
