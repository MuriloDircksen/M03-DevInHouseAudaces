using M03_Escola.Model;

namespace M03_Escola.Interfaces.Services
{
    public interface IMateriaService
    {
        public List<Materia> ObterMaterias();
        public Materia ObterPorId(int id);
        List<Materia> ObterPorNome(string nome);
        Materia Cadastrar(Materia materia);
        Materia Atualizar(Materia materia);
        void Excluir(int id);
    }
}
