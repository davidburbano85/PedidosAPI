using AutoMapper;

using Microsoft.AspNetCore.Mvc;


using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Models;
using Pedidos_API.Models.DTO;
using Pedidos_API.Infrastructura.ModelsPOCO;
using System.Net;
using Pedidos_API.Infrastructura.Repositorios;

namespace Canciones_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancionesController : ControllerBase
    {
        private readonly ILogger<CancionesController> _logger;
        private readonly IMapper _mapper;
        private readonly ICancionesRepositorio _CancionesRepositorio;
        protected ApiResponse _response;

        public CancionesController(ILogger<CancionesController> logger, ICancionesRepositorio CancionesRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _CancionesRepositorio = CancionesRepositorio;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetCancioness()
        {
            try
            {
                _logger.LogInformation("Obtener info de Cancioness");
                IEnumerable<Canciones> CancionessList = await _CancionesRepositorio.ObtenerTodos(null, x=> x.Empresa);

                _response.Resultado = _mapper.Map<IEnumerable<CancionesDto>>(CancionessList);
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetCancioness")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetCancioness(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer la licencia con ID: " + id);
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var Canciones = await _CancionesRepositorio.Obtener(v => v.id == id);

                if (Canciones == null)
                {
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<CancionesDto>(Canciones);
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
        public async Task<IActionResult> CrearCanciones(CrearCancionesDTO crearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var res = await _CancionesRepositorio.Obtener(v => v.linkcopiado.ToLower() == crearDto.linkcopiado.ToLower());
                if (res != null)
                {
                    ModelState.AddModelError("NombreExistente", "esta cancion ya existe");
                    return BadRequest(ModelState);
                }
                Canciones canc = _mapper.Map<Canciones>(crearDto);
                canc.nombreCancion = canc.nombreCancion;


                await _CancionesRepositorio.Crear(canc);
                _response.Resultado = canc;
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
        //[ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteCanciones(int id)
        {
            if (id == 0)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var Canciones = await _CancionesRepositorio.Obtener(v => v.id == id);
            if (Canciones == null)
            {
                return NotFound();
            }
            await _CancionesRepositorio.Remover(Canciones);

            return NoContent();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> UpdateCanciones(int id, CancionesDto CancionesDto)
        {
            if (CancionesDto == null || id != CancionesDto.id)
            {
                return BadRequest();
            }
            Canciones actual = _mapper.Map<Canciones>(CancionesDto);
            await _CancionesRepositorio.Modify(actual);
            return NoContent();
        }


    }
}
