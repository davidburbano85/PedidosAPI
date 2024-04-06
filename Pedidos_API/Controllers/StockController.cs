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
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IMapper _mapper;
        private readonly IStockRepositorio _StockRepositorio;
        protected ApiResponse _response;

        public StockController(ILogger<StockController> logger, IStockRepositorio StockRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _StockRepositorio = StockRepositorio;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetStock()
        {
            try
            {
                _logger.LogInformation("Obtener info de Productos Carta");
                IEnumerable<Stock> StockList = await _StockRepositorio.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<StockDto>>(StockList);
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

        public async Task<ActionResult<ApiResponse>> GetStock(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el producto con ID: " + id);
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var pedido = await _StockRepositorio.Obtener(v => v.id == id);

                if (pedido == null)
                {
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<StockDto>(pedido);
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
        public async Task<IActionResult> CrearPedido(CrearStockDTO crearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var res = await _StockRepositorio.Obtener(v => v.tipoProducto.ToLower() == crearDto.tipoProducto.ToLower());
                if (res != null)
                {
                    ModelState.AddModelError("NombreExistente", "ese producto ya existe");
                    return BadRequest(ModelState);
                }
                Stock pedi = _mapper.Map<Stock>(crearDto);
                pedi.tipoProducto = crearDto.tipoProducto;

                await _StockRepositorio.Crear(pedi);
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
            var pedido = await _StockRepositorio.Obtener(v => v.id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            await _StockRepositorio.Remover(pedido);

            return NoContent();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> UpdatePedido(int id, Stock StockDto)
        {
            if (StockDto == null || id != StockDto.id)
            {
                return BadRequest();
            }
            Stock actual = _mapper.Map<Stock>(StockDto);
            await _StockRepositorio.Modify(actual);
            return NoContent();
        }


    }
}
