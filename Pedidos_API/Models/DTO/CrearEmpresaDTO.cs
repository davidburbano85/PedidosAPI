using Pedidos_API.Infrastructura.Models;
using System.ComponentModel.DataAnnotations;

namespace Pedidos_API.Models.DTO
{
    public class CrearEmpresaDTO

    {
        [Key]
        public int id { get; set; }
        public string? nombreEstablecimiento { get; set; }
        public string? tipoEstablecimiento { get; set; }
        public int numeroMesas { get; set; }
        public DateTime fechaSuscripcion { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string? suscripcion { get; set; }

        //public List<Mesas>? historialMesas { get; set; }
    }
}
