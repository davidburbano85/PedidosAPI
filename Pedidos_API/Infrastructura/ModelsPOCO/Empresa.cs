using Pedidos_API.Infrastructura.ModelsPOCO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Infrastructura.Models
{

    public class Empresa

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? nombreEstablecimiento { get; set; }
        public string? tipoEstablecimiento { get; set; }
        public int numeroMesas { get; set; }
        public DateTime fechaSuscripcion { get; set;}
        public DateTime fechaVencimiento { get; set;}
        public string? suscripcion { get; set;}


        ////propiedad de navegacion de uno a muchos
        public virtual ICollection<Mesas> Mesas { get; set; }
        public virtual ICollection<Canciones> Canciones { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Consumo> consumos { get; set; }



    }

}
