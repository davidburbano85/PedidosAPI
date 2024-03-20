
namespace Pedidos_API.Models
{
    public class ApiResponse
    {
        public bool IsExitoso { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object? Resultado { get; set; }
        public object statusCode { get; internal set; }

        public ApiResponse()
        {
            ErrorMessages = new List<string>();
            IsExitoso = true;
        }
    }
}
