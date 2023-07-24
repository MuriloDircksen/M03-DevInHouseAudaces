using M03_Escola.DataBase.Repositories;
using M03_Escola.DTO;
using M03_Escola.Exceptions;
using M03_Escola.Interfaces.Repositories;
using M03_Escola.Interfaces.Services;
using M03_Escola.Model;

namespace M03_Escola.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository _materiaRepository;

        public MateriaService(IMateriaRepository materiaRepository)
        {
            _materiaRepository = materiaRepository;
        }
        public Materia Atualizar(Materia materia)
        {
            var materiaDb = _materiaRepository.ObterPorId(materia.Id);

            if (materiaDb == null)
            {
                throw new NotFoundException("Matéria não cadastrado");

            }
            materiaDb.Update(materia);

            _materiaRepository.Atualizar(materiaDb);
            return materiaDb;
        }

        public Materia Cadastrar(Materia materia)
        {
            if (_materiaRepository.ObterPorNome(materia.Nome) != null)
            {
                throw new RegistroDuplicadoException("Matéria já cadastrada");
            }

            _materiaRepository.Inserir(materia);
            return materia;
        }

        public void Excluir(int id)
        {
            var materia = _materiaRepository.ObterPorId(id) ?? throw new NotFoundException("Materia não cadastrado");
            
            _materiaRepository.Excluir(materia);
        }

        public List<Materia> ObterMaterias() => _materiaRepository.ObterTodos();
        

        public Materia ObterPorId(int id)
        {
            Materia materia = _materiaRepository.ObterPorId(id);

            if (materia == null)
            {
                throw new NotFoundException("Materia não encontrado");
            }
            return materia;
        }

        public List<Materia> ObterPorNome(string nome)
        {
            var materia = _materiaRepository.ObterPorNome(nome);

            return materia == null ? throw new NotFoundException("Materia não encontrado") : materia;
        }
    }
}
