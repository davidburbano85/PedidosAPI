﻿using Pedidos_API.Infrastructura.Models;
using Pedidos_API.Infrastructura.ModelsPOCO;
using System.ComponentModel.DataAnnotations;
namespace Pedidos_API.Models.DTO
{
    public class EmpresaDto
    {
        [Key]
        public int id { get; set; }
        public string? nombreEstablecimiento { get; set; }
        public string? tipoEstablecimiento { get; set; }
        public int numeroMesas { get; set; }
        public DateTime fechaSuscripcion { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string? suscripcion { get; set; }


    
       
    }
}
