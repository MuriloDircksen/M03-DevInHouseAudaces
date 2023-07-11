using M03_Escola.DTO;
using M03_Escola.Exceptions;
using M03_Escola.Interfaces.Services;
using M03_Escola.Model;
using Microsoft.AspNetCore.Mvc;

namespace M03_Escola.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoletimController : Controller
    {
        private readonly IBoletimService _boletimService;

        public BoletimController(IBoletimService boletimService)
        {
            _boletimService = boletimService;
        }

        [HttpPost("/alunos/{idAluno}/boletins")]
        public ActionResult Post(BoletimDTO boletim, int idAluno)
        {
            try
            {
                boletim.AlunoId = idAluno;

                boletim.Id = _boletimService.Cadastrar(new Boletim(boletim)).Id;

                //var boletimModel = _boletimService.Cadastrar(new Boletim(boletim));
                //boletim.Id = boletimModel.Id;

                return Ok(boletim);
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

        [HttpPut("/alunos/{idAluno}/boletins/{id}")]
        public ActionResult Put(BoletimDTO boletim, int idAluno, int id)
        {
            try
            {
                boletim.AlunoId = idAluno;

                boletim.Id = id;

                return Ok(new BoletimDTO(_boletimService.Atualizar(new Boletim(boletim))));
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

        [HttpGet("/alunos/{idAluno}/boletins")]
        public ActionResult GetPorAluno(int idAluno)
        {
            try
            {

                var boletins = _boletimService.ObterPorAluno(idAluno);
                return Ok(boletins.Select(x => new BoletimDTO(x)));
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

        [HttpGet("/alunos/{idAluno}/boletins/{id}")]
        public ActionResult GetPorIdValidaAluno(int idAluno, int id)
        {
            try
            {

                var boletim = _boletimService.ObterPorId(id);

                if (boletim.AlunoId != idAluno)
                    return NotFound("Boletim Id invalido para aluno");

                return Ok(new BoletimDTO(boletim));
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

        [HttpGet("/boletins/{id}")]
        public ActionResult GetPorId(int id)
        {
            try
            {

                var boletim = _boletimService.ObterPorId(id);

                return Ok(new BoletimDTO(boletim));
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

        [HttpDelete("boletins/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _boletimService.Excluir(id);

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
