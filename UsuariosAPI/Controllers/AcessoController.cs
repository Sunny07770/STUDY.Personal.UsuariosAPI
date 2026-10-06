using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AcessoController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "IdadeMinima")] // Esse endpoint só será acessível caso a condição da política de acesso seja satisfeita (nesse caso, idade mínima de 18 anos).
        public IActionResult Get()
        {
            return Ok("Acesso permitido!");
        }
    }
}
