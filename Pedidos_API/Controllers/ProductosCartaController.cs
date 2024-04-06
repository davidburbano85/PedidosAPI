using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedidos_API.Models;
using Pedidos_API.Models.DTO;
using Pedidos_API.Infrastructura.Models;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using System.Net;

namespace Pedidos_API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosCartaController : ControllerBase
    {
        private readonly ILogger<ProductosCartaController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductosCartaRepositorio _ProductosCartaRepositorio;
        protected ApiResponse _response;

        public ProductosCartaController(ILogger<ProductosCartaController> logger, IProductosCartaRepositorio ProductosCartaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _ProductosCartaRepositorio = ProductosCartaRepositorio;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetProductosCarta()
        {
            try
            {
                _logger.LogInformation("Obtener info de Productos Carta");
                IEnumerable<ProductosCarta> ProductosCartaList = await _ProductosCartaRepositorio.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<ProductosCartaDto>>(ProductosCartaList);
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetroductosCarta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetProductosCarta(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el producto con ID: " + id);
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var pedido = await _ProductosCartaRepositorio.Obtener(v => v.id == id);

                if (pedido == null)
                {
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<ProductosCartaDto>(pedido);
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPedido(CrearProductosCartaDTO crearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var res = await _ProductosCartaRepositorio.Obtener(v => v.nombreProducto.ToLower() == crearDto.nombreProducto.ToLower());
                if (res != null)
                {
                    ModelState.AddModelError("NombreExistente", "ese producto ya existe");
                    return BadRequest(ModelState);
                }
                ProductosCarta pedi = _mapper.Map<ProductosCarta>(crearDto);
                pedi.nombreProducto = crearDto.nombreProducto;

                await _ProductosCartaRepositorio.Crear(pedi);
                _response.Resultado = pedi;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeletePedido(int id)
        {
            if (id == 0)
            {
                //_response.IsExitoso=false;
                //_response.statusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var pedido = await _ProductosCartaRepositorio.Obtener(v => v.id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            await _ProductosCartaRepositorio.Remover(pedido);

            return NoContent();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> UpdatePedido(int id, ProductosCarta ProductosCartaDto)
        {
            if (ProductosCartaDto == null || id != ProductosCartaDto.id)
            {
                return BadRequest();
            }
            ProductosCarta actual = _mapper.Map<ProductosCarta>(ProductosCartaDto);
            await _ProductosCartaRepositorio.Modify(actual);
            return NoContent();
        }


    }
}
