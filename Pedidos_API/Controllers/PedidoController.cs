using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedidos_API.Models;
using Pedidos_API.Models.DTO;
using Pedidos_API.Infrastructura.Models;
using Pedidos_API.Infrastructura.ContractsOInterfaces;

namespace Pedidos_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IMapper _mapper;
        private readonly IPedidoRepositorio _pedidoRepositorio;
        protected ApiResponse _response;

        public PedidoController(ILogger<PedidoController> logger, IPedidoRepositorio pedidoRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _pedidoRepositorio = pedidoRepositorio;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetPedidos()
        {
            try
            {
                _logger.LogInformation("Obtener info de Pedidos");
                IEnumerable<Pedidos> pedidosList = await _pedidoRepositorio.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<PedidosDto>>(pedidosList);
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetPedidos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetPedidos(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer la licencia con ID: " + id);
                    return BadRequest(_response);
                }
                var pedido = await _pedidoRepositorio.Obtener(v => v.Id == id);

                if (pedido == null)
                {
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<PedidosDto>(pedido);
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
        public async Task<IActionResult> CrearPedido(CrearPedidosDTO crearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var res = await _pedidoRepositorio.Obtener(v => v.Nombre.ToLower() == crearDto.Nombre.ToLower());
                if (res != null)
                {
                    ModelState.AddModelError("NombreExistente", "ese producto ya existe");
                    return BadRequest(ModelState);
                }
                Pedidos pedi=_mapper.Map<Pedidos>(crearDto);
                pedi.FechadeLlegada=DateTime.Now;

                await _pedidoRepositorio.Crear(pedi);
                _response.Resultado= pedi;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete ("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var pedido = await _pedidoRepositorio.Obtener(v => v.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            _pedidoRepositorio.Remover(pedido);

            return NoContent();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> UpdatePedido(int id, PedidosDto pedidoDto)
        {
            if (pedidoDto == null || id != pedidoDto.Id)
            {
                return BadRequest();
            }
            Pedidos actual = _mapper.Map<Pedidos>(pedidoDto);
            _pedidoRepositorio.Modify(actual);
            return NoContent();
        }


    }
}
