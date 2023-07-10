using M03_Escola.Interfaces.Repositories;
using M03_Escola.Model;

namespace M03_Escola.DataBase.Repositories
{
    public class AlunoRepository : BaseRepository<Aluno, int>, IAlunoRepository
    {
        public AlunoRepository(EscolaDBContexto contexto) : base(contexto)
        {
        }

        public override Aluno ObterPorId(int id)
        {
            return _context.Alunos.FirstOrDefault(x => id == x.Id);
        }

        public bool EmailJaCadastrado(string email)
            => _context.Alunos.Any(x => x.Email == email);

    }
    
}
