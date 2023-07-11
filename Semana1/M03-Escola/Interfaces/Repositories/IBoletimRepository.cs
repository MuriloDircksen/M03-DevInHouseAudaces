using M03_Escola.Model;

namespace M03_Escola.Interfaces.Repositories
{
    public interface IBoletimRepository : IBaseRepository<Boletim, int>
    {
        List<Boletim> ObterPorAlunoId(int alunoId);

    }
}
