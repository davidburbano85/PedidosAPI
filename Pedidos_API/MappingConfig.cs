
using AutoMapper;
using Pedidos_API.Infrastructura.Models;
using Pedidos_API.Models.DTO;

namespace Pedidos_API
{
    public class MappingConfig : Profile
    {
        public  MappingConfig()
        {
            CreateMap< Pedidos, PedidosDto >().ReverseMap();

            CreateMap<Pedidos, CrearPedidosDTO>().ReverseMap();
        }
    }
}
