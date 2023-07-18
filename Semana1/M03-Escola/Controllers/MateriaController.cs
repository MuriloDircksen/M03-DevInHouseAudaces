using M03_Escola.DTO;
using M03_Escola.Exceptions;
using M03_Escola.Interfaces.Services;
using M03_Escola.Model;
using M03_Escola.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M03_Escola.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MateriaController : Controller
    {
        private readonly IMateriaService _materiaService;

        public MateriaController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public ActionResult Post(MateriaDTO materia)
        {            
                
               var  materiaDB = _materiaService.Cadastrar(new Materia(materia));
                               
                return Ok(new MateriaDTO(materiaDB));
            
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor")]
        public ActionResult Put(MateriaDTO materia, int id)
        {                          

                materia.Id = id;

                return Ok(new MateriaDTO(_materiaService.Atualizar(new Materia(materia))));
            
        }

        [HttpGet("{nome}")]
        [Authorize(Roles = "Professor, Aluno")]
        public ActionResult GetPornome(string nome)
        {            
                var materias = _materiaService.ObterPorNome(nome);
                return Ok(materias.Select(x => new MateriaDTO(x)));
            
        }        

        [HttpGet("{id}")]
        [Authorize(Roles = "Professor, Aluno")]
        public ActionResult GetPorId(int id)
        {            
                var materia = _materiaService.ObterPorId(id);

                return Ok(new MateriaDTO(materia));
            
        }
        [HttpGet]
        [Authorize(Roles = "Professor, Aluno")]
        public ActionResult Get()
        {            
                var materias = _materiaService.ObterMaterias();
                IEnumerable<MateriaDTO> materiasDTO = materias.Select(x => new MateriaDTO(x));
                return Ok(materiasDTO);
            
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor")]
        public ActionResult Delete(int id)
        {            
                _materiaService.Excluir(id);

                return StatusCode(204);
            
        }
    }
}
