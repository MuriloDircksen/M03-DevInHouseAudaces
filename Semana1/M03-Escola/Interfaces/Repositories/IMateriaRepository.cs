using M03_Escola.Model;

namespace M03_Escola.Interfaces.Repositories
{
    public interface IMateriaRepository : IBaseRepository<Materia, int>
    {
        List<Materia> ObterPorNome(string nome);
    }
}
