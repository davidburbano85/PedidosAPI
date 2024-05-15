using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.ModelsPOCO;
using Pedidos_API.Models;
using Pedidos_API.Models.DTO;
using System.Linq.Expressions;
using System.Net;

namespace Consumos_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoController : ControllerBase
    {
        private readonly ILogger<ConsumoController> _logger;
        private readonly IMapper _mapper;
        private readonly IConsumoRepositorio _ConsumoRepositorio;
        protected ApiResponse _response;
        private readonly ApplicationDbContext _context;     

        public ConsumoController(ILogger<ConsumoController> logger, IConsumoRepositorio ConsumoRepositorio, IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _ConsumoRepositorio = ConsumoRepositorio;
            _response = new();
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetConsumos()
        {
            try
            {

                _logger.LogInformation("Obtener info de Consumos");
                var arrayConsumos = new List<Expression<Func<Consumo, object>>>
                {
                    e=>e.Empresa,
                    m=>m.Mesas,
                    p=>p.ProductosCarta
                };

                IEnumerable<Consumo> ConsumosList = await _ConsumoRepositorio.ObtenerTodos(null, arrayConsumos.ToArray());

                _response.Resultado = _mapper.Map<IEnumerable<ConsumoDto>>(ConsumosList);
            
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetConsumos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetConsumos(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer la licencia con ID: " + id);
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var arrayConsumos = new List<Expression<Func<Consumo, object>>>
                {
                    e=>e.Empresa,
                    m=>m.Mesas,
                    p=>p.ProductosCarta,
                 
                };
                var Consumo = await _ConsumoRepositorio.Obtener(v => v.id == id, true, arrayConsumos.ToArray());

                if (Consumo == null)
                {
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<ConsumoDto>(Consumo);
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



        public async Task<IActionResult> CrearConsumo(CrearConsumoDTO crearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var res = await _ConsumoRepositorio.Obtener(v => v.idMesa == crearDto.idMesa);
                if (res != null)
                {
                    ModelState.AddModelError("NombreExistente", "ese producto ya existe");
                    return BadRequest(ModelState);
                }
                Consumo consu = _mapper.Map<Consumo>(crearDto);
                consu.cantidad = consu.cantidad;

                await _ConsumoRepositorio.Crear(consu);
                _response.Resultado = consu;
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

        public async Task<IActionResult> DeleteConsumo(int id)
        {
            if (id == 0)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var Consumo = await _ConsumoRepositorio.Obtener(v => v.id == id);
            if (Consumo == null)
            {
                return NotFound();
            }
            await _ConsumoRepositorio.Remover(Consumo);

            return NoContent();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> UpdateConsumo(int id, ConsumoDto ConsumoDto)
        {
            if (ConsumoDto == null || id != ConsumoDto.id)
            {
                return BadRequest();
            }
            Consumo actual = _mapper.Map<Consumo>(ConsumoDto);
            await _ConsumoRepositorio.Modify(actual);
            return NoContent();
        }


    }
}
