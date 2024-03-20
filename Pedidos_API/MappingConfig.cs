
using AutoMapper;
using Pedidos_API.Infrastructura.Models;
using Pedidos_API.Infrastructura.ModelsPOCO;
using Pedidos_API.Models.DTO;

namespace Pedidos_API
{
    public class MappingConfig : Profile
    {
        public  MappingConfig()
        {
            CreateMap< Pedidos, PedidosDto >().ReverseMap();
            CreateMap<Pedidos, CrearPedidosDTO>().ReverseMap();
            
            CreateMap<Consumo, ConsumoDto>().ReverseMap();
            CreateMap<Consumo, CrearConsumoDTO>().ReverseMap();

            CreateMap<Password, PasswordDto>().ReverseMap();
            CreateMap<Password, CrearPasswordDTO>().ReverseMap();

            CreateMap<Canciones, CancionesDto>().ReverseMap();
            CreateMap<Canciones, CrearCancionesDTO>().ReverseMap();
        }
    }
}
