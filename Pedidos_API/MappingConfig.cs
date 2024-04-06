
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
            CreateMap< ProductosCarta, ProductosCartaDto>().ReverseMap();
            CreateMap<ProductosCarta, CrearProductosCartaDTO>().ReverseMap();
            
            CreateMap<Consumo, ConsumoDto>().ReverseMap();
            CreateMap<Consumo, CrearConsumoDTO>().ReverseMap();

            CreateMap<Usuarios, UsuariosDto>().ReverseMap();
            CreateMap<Usuarios, CrearUsuariosDTO>().ReverseMap();

            CreateMap<Canciones, CancionesDto>().ReverseMap();
            CreateMap<Canciones, CrearCancionesDTO>().ReverseMap();

            CreateMap<Empresa, EmpresaDto>().ReverseMap();
            CreateMap<Empresa, CrearEmpresaDTO>().ReverseMap();

            CreateMap<Mesas, MesasDto>().ReverseMap();
            CreateMap<Mesas, CrearMesasDTO>().ReverseMap();

            CreateMap<Stock, StockDto>().ReverseMap();
            CreateMap<Stock, CrearStockDTO>().ReverseMap();
        }
    }
}
