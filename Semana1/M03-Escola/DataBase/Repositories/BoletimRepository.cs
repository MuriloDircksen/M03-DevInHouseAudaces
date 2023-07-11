using M03_Escola.Interfaces.Repositories;
using M03_Escola.Model;

namespace M03_Escola.DataBase.Repositories
{
    public class BoletimRepository : BaseRepository<Boletim, int>, IBoletimRepository
    {
        public BoletimRepository(EscolaDBContexto contexto) : base(contexto)
        {
        }

        public List<Boletim> ObterPorAlunoId(int alunoId)
        {
            return _context.Set<Boletim>().Where(x => x.AlunoId == alunoId).ToList();
        }
    }
}
