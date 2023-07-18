using M03_Escola.DTO;
using M03_Escola.Exceptions;
using M03_Escola.Interfaces.Services;
using M03_Escola.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M03_Escola.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class NotasMateriaController : Controller
    {
        private readonly INotasMateriaService _notasMateriaService;

        public NotasMateriaController(INotasMateriaService notasMateriaService)
        {
            _notasMateriaService = notasMateriaService;
        }

        [HttpPost]
        public ActionResult Post(NotasMateriaDTO notasMateria)
        {
                var notasMateriaDB = _notasMateriaService.Cadastrar(new NotasMateria(notasMateria));

                return Ok(new NotasMateriaDTO(notasMateriaDB));
           
        }

        [HttpPut("{id}")]
        public ActionResult Put(NotasMateriaDTO notasMateria, int id)
        {
                notasMateria.Id = id;

                return Ok(new NotasMateriaDTO(_notasMateriaService.Atualizar(new NotasMateria(notasMateria))));
            
        }
              
        [HttpGet("{id}")]
        public ActionResult GetPorId(int id)
        {
                var notasMateria = _notasMateriaService.ObterPorId(id);

                return Ok(new NotasMateriaDTO(notasMateria));
            
        }
        [HttpGet]
        public ActionResult Get()
        {
                var notasMaterias = _notasMateriaService.ObterNotasMaterias();
                IEnumerable<NotasMateriaDTO> notasMateriasDTO = notasMaterias.Select(x => new NotasMateriaDTO(x));
                return Ok(notasMateriasDTO);            
            
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {            
                _notasMateriaService.Excluir(id);

                return StatusCode(204);
            
        }
    }
}
