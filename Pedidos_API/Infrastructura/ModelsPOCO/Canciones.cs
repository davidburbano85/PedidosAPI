﻿using Pedidos_API.Infrastructura.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Infrastructura.ModelsPOCO
{
    public class Canciones
    {
        [Key]

        public int id { get; set; }
        [Required]
        [ForeignKey("Empresa")]


        public int idEmpresa { get; set; }
        [Required]
        [ForeignKey("Mesas")]

        public int idMesa { get; set; }       
       
        public string? linkcopiado { get; set; }
        public string? nombreCancion { get; set; }

       
        public virtual Mesas Mesas { get; set; }

        public virtual Empresa Empresa { get; set; }


    }
}
