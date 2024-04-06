using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedidos_API.Models;
using Pedidos_API.Models.DTO;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using System.Net;
using Pedidos_API.Infrastructura.Models;
using System;
using System.Linq.Expressions;

namespace Mesas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly ILogger<MesasController> _logger;
        private readonly IMapper _mapper;
        private readonly IMesasRepositorio _MesasRepositorio;
        protected ApiResponse _response;

        public MesasController(ILogger<MesasController> logger, IMesasRepositorio MesasRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _MesasRepositorio = MesasRepositorio;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetMesas()
        {
            try
            {
                int idEmpresaEntrante;
                
                _logger.LogInformation("Obtener info de Mesas");
                IEnumerable<Mesas> MesasList = await _MesasRepositorio.ObtenerTodos(null, x=>x.Empresa);
               

                _response.Resultado = _mapper.Map<IEnumerable<MesasDto>>(MesasList);
               
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetMesas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetMesas(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer la mesa con ID: " + id);
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var tracked = true;
                var arrays = new List<Expression<Func<Mesas, object>>>
                {
                    x => x.Empresa
                };
                var Mesa = await _MesasRepositorio.Obtener(v => v.id == id, tracked, arrays.ToArray());

                if (Mesa == null)
                {
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<MesasDto>(Mesa);
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
        public async Task<IActionResult> CrearMesas(CrearMesasDTO crearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var res = await _MesasRepositorio.Obtener(v => v.estado.ToLower() == crearDto.estado.ToLower());
                if (res != null)
                {
                    ModelState.AddModelError("NombreExistente", "esa mesa ya existe");
                    return BadRequest(ModelState);
                }
                Mesas pedi = _mapper.Map<Mesas>(crearDto);
                pedi.fechaInicio = DateTime.Now;

                await _MesasRepositorio.Crear(pedi);
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

        public async Task<IActionResult> DeleteMesas(int id)
        {
            if (id == 0)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var Mesa = await _MesasRepositorio.Obtener(v => v.id == id);
            if (Mesa == null)
            {
                return NotFound();
            }
            await _MesasRepositorio.Remover(Mesa);

            return NoContent();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> UpdateMesas(int id, MesasDto MesasDto)
        {
            if (MesasDto == null || id != MesasDto.id)
            {
                return BadRequest();
            }
            Mesas actual = _mapper.Map<Mesas>(MesasDto);
            await _MesasRepositorio.Modify(actual);
            return NoContent();
        }


    }
}
