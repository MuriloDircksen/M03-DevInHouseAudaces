using M03_Escola.Model;

namespace M03_Escola.Interfaces.Services
{
    public interface IBoletimService
    {
        public Boletim ObterPorId(int id);
        public List<Boletim> ObterPorAluno(int alunoId);
        Boletim Cadastrar(Boletim boletim);
        Boletim Atualizar(Boletim boletim);
        void Excluir(int id);
    }
}
