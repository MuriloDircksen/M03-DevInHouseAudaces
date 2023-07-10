using M03_Escola.Interfaces.Repositories;
using M03_Escola.Model;

namespace M03_Escola.DataBase.Repositories
{
    public class TurmaRepository : BaseRepository<Turma, int>, ITurmaRepository
    {
        public TurmaRepository(EscolaDBContexto contexto) : base(contexto)
        {
        }
    }
}
