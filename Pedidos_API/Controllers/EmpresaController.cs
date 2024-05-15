using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Models;
using Pedidos_API.Infrastructura.Models;
using Pedidos_API.Models.DTO;
using Pedidos_API.Infrastructura.BaseRespository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Empresa_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly ILogger<EmpresaController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmpresaRepositorio _EmpresaRepositorio;
        private readonly ApplicationDbContext _context;
        protected ApiResponse _response;

        public EmpresaController(ILogger<EmpresaController> logger, IEmpresaRepositorio EmpresaRepositorio, IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _EmpresaRepositorio = EmpresaRepositorio;
            _response = new();
            _context = context;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetEmpresa()
        {
            try
            {
                _logger.LogInformation("Obtener info de Empresa");
                IEnumerable<Empresa> EmpresaList = await _EmpresaRepositorio.ObtenerTodos();




                _response.Resultado = _mapper.Map<IEnumerable<EmpresaDto>>(EmpresaList);



                return Ok(_response);
          

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetEmpresa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetEmpresa(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer la licencia con ID: " + id);
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var Empresa = await _EmpresaRepositorio.Obtener(v => v.id == id);

                if (Empresa == null)
                {
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<EmpresaDto>(Empresa);
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
        public async Task<IActionResult> CrearEmpresa(CrearEmpresaDTO crearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var res = await _EmpresaRepositorio.Obtener(v => v.nombreEstablecimiento.ToLower() == crearDto.nombreEstablecimiento.ToLower());
                if (res != null)
                {
                    ModelState.AddModelError("NombreExistente", "ese producto ya existe");
                    return BadRequest(ModelState);
                }
                Empresa pedi = _mapper.Map<Empresa>(crearDto);
                pedi.fechaSuscripcion = DateTime.Now;

                await _EmpresaRepositorio.Crear(pedi);
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

        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            if (id == 0)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var Empresa = await _EmpresaRepositorio.Obtener(v => v.id == id);
            if (Empresa == null)
            {
                return NotFound();
            }
            await _EmpresaRepositorio.Remover(Empresa);

            return NoContent();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> UpdateEmpresa(int id, EmpresaDto EmpresaDto)
        {
            if (EmpresaDto == null || id != EmpresaDto.id)
            {
                return BadRequest();
            }
            Empresa actual = _mapper.Map<Empresa>(EmpresaDto);
            await _EmpresaRepositorio.Modify(actual);
            return NoContent();
        }


    }
}
