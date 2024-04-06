using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pedidos_API.Infrastructura.ModelsPOCO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Infrastructura.Models
{

    public class ProductosCarta

    {
        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey("Empresa")]
        public int idEmpresa { get; set; }
        [Required]
        [ForeignKey("Stock")]
        public int idStock { get; set; }
        public string nombreProducto { get; set; }  
        public string tipoProducto { get; set; }  
        public string unidad { get; set; }

        //crear relaciones foraneas
        // uno a muchos
        public virtual Empresa Empresa { get; set; }
        public virtual Stock Stock { get; set; }
        //muchos a uno

        public virtual ICollection<Consumo> Consumos { get; set; }







    }

}
