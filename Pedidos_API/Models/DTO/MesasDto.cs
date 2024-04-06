using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pedidos_API.Infrastructura.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Models.DTO
{
    public class MesasDto
    {
        [Key]

        public int id { get; set; }
        [Required]
        [ForeignKey("Empresa")]

        public int idEmpresa { get; set; }
        public int numeroMesa { get; set; }
        public int sillas { get; set; }
        public string? estado { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }

        //propiedad de navegacion
        public virtual Empresa? Empresa { get; set; }



    }
}
