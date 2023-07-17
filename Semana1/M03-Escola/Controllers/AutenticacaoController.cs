using M03_Escola.DTO;
using M03_Escola.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M03_Escola.Controllers
{
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {

        private readonly IAutenticacaoServices _autenticacaoServices;
        public AutenticacaoController(IAutenticacaoServices autenticacaoServices)
        {
            _autenticacaoServices = autenticacaoServices;
        }
        [HttpPost("/login")]
        [AllowAnonymous]
        public ActionResult Logar(LoginDTO loginDTO)
        {
            return Ok(new { token = _autenticacaoServices.Autenticar(loginDTO) });
        }
    }
}
