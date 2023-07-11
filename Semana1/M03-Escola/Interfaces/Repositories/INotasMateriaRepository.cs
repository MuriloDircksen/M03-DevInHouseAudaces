using M03_Escola.Model;

namespace M03_Escola.Interfaces.Repositories
{
    public interface INotasMateriaRepository : IBaseRepository<NotasMateria, int>
    {
        List<NotasMateria> ObterPorBoletimId(int boletimId);
    }
}
