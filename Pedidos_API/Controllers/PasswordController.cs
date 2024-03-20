using AutoMapper;

using Microsoft.AspNetCore.Mvc;


using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Models;
using Pedidos_API.Models.DTO;
using Pedidos_API.Infrastructura.ModelsPOCO;

namespace Password_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly ILogger<PasswordController> _logger;
        private readonly IMapper _mapper;
        private readonly IPasswordRepositorio _PasswordRepositorio;
        protected ApiResponse _response;

        public PasswordController(ILogger<PasswordController> logger, IPasswordRepositorio PasswordRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _PasswordRepositorio = PasswordRepositorio;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetPasswords()
        {
            try
            {
                _logger.LogInformation("Obtener info de Passwords");
                IEnumerable<Password> PasswordsList = await _PasswordRepositorio.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<PasswordDto>>(PasswordsList);
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetPasswords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetPasswords(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer la licencia con ID: " + id);
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var Password = await _PasswordRepositorio.Obtener(v => v.IdPass == id);

                if (Password == null)
                {
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<PasswordDto>(Password);
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
        public async Task<IActionResult> CrearPassword(CrearPasswordDTO crearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var res = await _PasswordRepositorio.Obtener(v => v.Nombre.ToLower() == crearDto.Nombre.ToLower());
                if (res != null)
                {
                    ModelState.AddModelError("NombreExistente", "ese producto ya existe");
                    return BadRequest(ModelState);
                }
                Password pas=_mapper.Map<Password>(crearDto);
                pas.Pass="";

                await _PasswordRepositorio.Crear(pas);
                _response.Resultado= pas;
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
        
        public async Task<IActionResult> DeletePassword(int id)
        {
            if (id == 0)
            {
                //_response.IsExitoso=false;
                //_response.statusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var Password = await _PasswordRepositorio.Obtener(v => v.IdPass == id);
            if (Password == null)
            {
                return NotFound();
            }
            await _PasswordRepositorio.Remover(Password);

            return NoContent();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> UpdatePassword(int id, PasswordDto PasswordDto)
        {
            if (PasswordDto == null || id != PasswordDto.IdPass)
            {
                return BadRequest();
            }
            Password actual = _mapper.Map<Password>(PasswordDto);
            await _PasswordRepositorio.Modify(actual);
            return NoContent();
        }


    }
}
