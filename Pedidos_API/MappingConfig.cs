
using AutoMapper;
using Pedidos_API.Models;
using Pedidos_API.Models.DTO;

namespace Pedidos_API
{
    public class MappingConfig : Profile
    {
        public  MappingConfig()
        {
            CreateMap< Pedidoss, PedidosDto >().ReverseMap();

            CreateMap<Pedidoss, CrearPedidosDTO>().ReverseMap();
        }
    }
}
