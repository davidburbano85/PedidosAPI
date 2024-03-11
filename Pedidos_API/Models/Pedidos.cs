﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Models
{
    
    public class Pedidoss

    {
        [Key]
   
        public int Id { get; set; }
        public string? Nombre { get; set;}
        [Required]
        public int Cantidad { get; set; }

        public int Precio { get; set; }     

        public DateTime FechadeLlegada { get; set; }



    }
}
