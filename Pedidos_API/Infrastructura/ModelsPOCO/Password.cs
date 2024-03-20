using Pedidos_API.Infrastructura.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_API.Infrastructura.ModelsPOCO
{
    public class Password
    {
        [Key]

        public int IdPass { get; set; }
       
        public string? Nombre { get; set; }
       
        public string? Pass { get; set; }

     

      
    }
}
