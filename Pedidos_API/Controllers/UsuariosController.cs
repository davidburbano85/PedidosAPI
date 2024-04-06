using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Models;
using Pedidos_API.Models.DTO;
using Pedidos_API.Infrastructura.ModelsPOCO;
using Pedidos_API;
using System.Net;

namespace Usuarios_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsuariosRepositorio _UsuariosRepositorio;
        protected ApiResponse _response;

        public UsuariosController(ILogger<UsuariosController> logger, IUsuariosRepositorio UsuariosRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _UsuariosRepositorio = UsuariosRepositorio;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetUsuarios()
        {
            try
            {
                _logger.LogInformation("Obtener info de Usuarios");
                IEnumerable<Usuarios> UsuariosList = await _UsuariosRepositorio.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<UsuariosDto>>(UsuariosList);
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("GetUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetUsuarios(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el usuario con noombre: " + id);
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var Usuarios = await _UsuariosRepositorio.Obtener(v => v.id == id);

                if (Usuarios == null)
                {
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<UsuariosDto>(Usuarios);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CrearUsuario(CrearUsuariosDTO crearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var res = await _UsuariosRepositorio.Obtener(v => v.nombre.ToLower() == crearDto.nombre.ToLower());
                if (res != null)
                {
                    ModelState.AddModelError("NombreExistente", "ese usuario ya existe");
                    return BadRequest(ModelState);
                }
                string hashedUsuario = Encript.GetSHA256(crearDto.contraseña);
                Usuarios pasw = _mapper.Map<Usuarios>(crearDto);
                pasw.contraseña = hashedUsuario;

                //Usuario pas=_mapper.Map<Usuario>(crearDto);
                //pas.Pass=pas.Pass;

                await _UsuariosRepositorio.Crear(pasw);
                _response.Resultado = pasw;
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

        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (id==0)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var Usuario = await _UsuariosRepositorio.Obtener(v => v.id == id);
            if (Usuario == null)
            {
                return NotFound();
            }
            await _UsuariosRepositorio.Remover(Usuario);

            return NoContent();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> UpdateUsuario(int id, UsuariosDto UsuarioDto)
        {
            if (UsuarioDto == null || id != UsuarioDto.id)
            {
                return BadRequest();
            }

            // Mapear DTO a la entidad Usuario
            Usuarios actual = _mapper.Map<Usuarios>(UsuarioDto);

            // Calcular el hash SHA-256 de la nueva contraseña
            string hashedUsuario = Encript.GetSHA256(actual.contraseña);
            actual.contraseña = hashedUsuario;

            // Actualizar la contraseña en el repositorio
            await _UsuariosRepositorio.Modify(actual);
            await _UsuariosRepositorio.Modify(actual);
            return NoContent();
        }


    }
}
