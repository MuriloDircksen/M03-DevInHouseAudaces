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
    public class BoletimController : Controller
    {
        private readonly IBoletimService _boletimService;

        public BoletimController(IBoletimService boletimService)
        {
            _boletimService = boletimService;
        }

        [HttpPost("/alunos/{idAluno}/boletins")]
        [Authorize(Roles = "Professor")]
        public ActionResult Post(BoletimDTO boletim, int idAluno)
        {
            
                boletim.AlunoId = idAluno;

                boletim.Id = _boletimService.Cadastrar(new Boletim(boletim)).Id;
                                

                return Ok(boletim);
            
        }

        [HttpPut("/alunos/{idAluno}/boletins/{id}")]
        [Authorize(Roles = "Professor")]
        public ActionResult Put(BoletimDTO boletim, int idAluno, int id)
        {
            
                boletim.AlunoId = idAluno;

                boletim.Id = id;

                return Ok(new BoletimDTO(_boletimService.Atualizar(new Boletim(boletim))));
            
        }

        [HttpGet("/alunos/{idAluno}/boletins")]
        [Authorize(Roles = "Professor, Aluno")]
        public ActionResult GetPorAluno(int idAluno)
        {
            

                var boletins = _boletimService.ObterPorAluno(idAluno);
                return Ok(boletins.Select(x => new BoletimDTO(x)));
            
        }

        [HttpGet("/alunos/{idAluno}/boletins/{id}")]
        [Authorize(Roles = "Professor, Aluno")]
        public ActionResult GetPorIdValidaAluno(int idAluno, int id)
        {
            
                var boletim = _boletimService.ObterPorId(id);

                if (boletim.AlunoId != idAluno)
                    return NotFound("Boletim Id invalido para aluno");

                return Ok(new BoletimDTO(boletim));
            
        }

        [HttpGet("/boletins/{id}")]
        [Authorize(Roles = "Professor, Aluno")]
        public ActionResult GetPorId(int id)
        {
            

                var boletim = _boletimService.ObterPorId(id);

                return Ok(new BoletimDTO(boletim));
            
        }

        [HttpDelete("boletins/{id}")]
        [Authorize(Roles = "Professor")]
        public ActionResult Delete(int id)
        {
            
                _boletimService.Excluir(id);

                return StatusCode(204);
            
        }
    }
}
