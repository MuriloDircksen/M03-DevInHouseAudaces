using M03_Escola.Model;
namespace M03_Escola.Interfaces.Repositories
{
    public interface IAlunoRepository : IBaseRepository<Aluno, int>
    {
        public bool EmailJaCadastrado(string email);
    }
}
