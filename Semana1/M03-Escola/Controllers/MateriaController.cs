using M03_Escola.DTO;
using M03_Escola.Exceptions;
using M03_Escola.Interfaces.Services;
using M03_Escola.Model;
using M03_Escola.Services;
using Microsoft.AspNetCore.Mvc;

namespace M03_Escola.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MateriaController : Controller
    {
        private readonly IMateriaService _materiaService;

        public MateriaController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        [HttpPost]
        public ActionResult Post(MateriaDTO materia)
        {
            try
            {
                
               var  materiaDB = _materiaService.Cadastrar(new Materia(materia));
                               
                return Ok(new MateriaDTO(materiaDB));
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
        public ActionResult Put(MateriaDTO materia, int id)
        {
            try
            {              

                materia.Id = id;

                return Ok(new MateriaDTO(_materiaService.Atualizar(new Materia(materia))));
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

        [HttpGet("{nome}")]
        public ActionResult GetPornome(string nome)
        {
            try
            {
                var materias = _materiaService.ObterPorNome(nome);
                return Ok(materias.Select(x => new MateriaDTO(x)));
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
                var materia = _materiaService.ObterPorId(id);

                return Ok(new MateriaDTO(materia));
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
                var materias = _materiaService.ObterMaterias();
                IEnumerable<MateriaDTO> materiasDTO = materias.Select(x => new MateriaDTO(x));
                return Ok(materiasDTO);
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
                _materiaService.Excluir(id);

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
