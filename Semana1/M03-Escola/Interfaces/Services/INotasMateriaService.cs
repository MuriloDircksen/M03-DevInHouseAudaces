using M03_Escola.Model;

namespace M03_Escola.Interfaces.Services
{
    public interface INotasMateriaService
    {
        public List<NotasMateria> ObterNotasMaterias();
        public NotasMateria ObterPorId(int id);
        List<NotasMateria> ObterPorBoletimId(int boletimId);
        NotasMateria Atualizar(NotasMateria notasMateria);
        void Excluir(int id);
    }
}
