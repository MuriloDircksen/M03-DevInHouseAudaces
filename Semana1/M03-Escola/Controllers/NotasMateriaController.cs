using M03_Escola.DTO;
using M03_Escola.Exceptions;
using M03_Escola.Interfaces.Services;
using M03_Escola.Model;
using Microsoft.AspNetCore.Mvc;

namespace M03_Escola.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            try
            {
                var notasMateriaDB = _notasMateriaService.Cadastrar(new NotasMateria(notasMateria));

                return Ok(new NotasMateriaDTO(notasMateriaDB));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(NotasMateriaDTO notasMateria, int id)
        {
            try
            {

                notasMateria.Id = id;

                return Ok(new NotasMateriaDTO(_notasMateriaService.Atualizar(new NotasMateria(notasMateria))));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
              
        [HttpGet("{id}")]
        public ActionResult GetPorId(int id)
        {
            try
            {
                var notasMateria = _notasMateriaService.ObterPorId(id);

                return Ok(new NotasMateriaDTO(notasMateria));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var notasMaterias = _notasMateriaService.ObterNotasMaterias();
                IEnumerable<NotasMateriaDTO> notasMateriasDTO = notasMaterias.Select(x => new NotasMateriaDTO(x));
                return Ok(notasMateriasDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _notasMateriaService.Excluir(id);

                return StatusCode(204);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
