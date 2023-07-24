using M03_Escola.DataBase.Repositories;
using M03_Escola.Exceptions;
using M03_Escola.Interfaces.Repositories;
using M03_Escola.Interfaces.Services;
using M03_Escola.Model;
using System.Runtime.ConstrainedExecution;

namespace M03_Escola.Services
{
    public class NotasMateriaService : INotasMateriaService
    {
        private readonly INotasMateriaRepository _notasMateriaRepository;
        public NotasMateriaService(INotasMateriaRepository notasMateriaRepository)
        {
            _notasMateriaRepository = notasMateriaRepository;
        }

        public NotasMateria Atualizar(NotasMateria notasMateria)
        {
            var notasMateriaDb = _notasMateriaRepository.ObterPorId(notasMateria.Id) ?? throw new NotFoundException("Relação Notas Matéria não cadastrado");
            
           
            notasMateriaDb.Update(notasMateria);

            _notasMateriaRepository.Atualizar(notasMateriaDb);
            return notasMateriaDb;
        }

        public NotasMateria Cadastrar(NotasMateria notasMateria)
        {
            ValidaNotas(notasMateria);
            _notasMateriaRepository.Inserir(notasMateria);
            return notasMateria;
        }

        private static void ValidaNotas(NotasMateria notasMateria)
        {
            if (notasMateria.Nota < 0)
            {
                throw new ArgumentOutOfRangeException("Nota", notasMateria.Nota, "Nota deve ser maior que zero");
            }
            else if (notasMateria.Nota > 10)
            {
                throw new ArgumentOutOfRangeException("Nota", notasMateria.Nota, "Nota deve ser menor ou igual a 10");
            }
        }

        public void Excluir(int id)
        {
            var notasMateria = _notasMateriaRepository.ObterPorId(id) ?? throw new NotFoundException("Relação Notas Materia não cadastrado");

            _notasMateriaRepository.Excluir(notasMateria);
        }

        public List<NotasMateria> ObterNotasMaterias() => _notasMateriaRepository.ObterTodos();
        
        public List<NotasMateria> ObterPorBoletimId(int boletimId)
        {
            var notasMateria = _notasMateriaRepository.ObterPorBoletimId(boletimId);

            return notasMateria ?? throw new NotFoundException("Relação Notas Materia não encontrado");
        }

        public NotasMateria ObterPorId(int id)
        {
            var notasMateria = _notasMateriaRepository.ObterPorId(id) ?? throw new NotFoundException("Relação Notas Materia não encontrado");
            return notasMateria;
        }        
    }
}
