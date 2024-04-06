﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pedidos_API.Infrastructura.ModelsPOCO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Infrastructura.Models
{

    public class Mesas

    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Empresa")]
        public int idEmpresa { get; set; }
        public int numeroMesa { get; set; }
        public  int sillas { get; set; }
        public string? estado  { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }

        //propiedad de navegacion
        public virtual Empresa Empresa { get; set; }
        public virtual List<Canciones> Canciones { get; set; }
       public virtual List<Consumo> Consumo { get; set; }






    }

}
