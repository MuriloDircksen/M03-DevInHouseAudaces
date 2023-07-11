using M03_Escola.Interfaces.Repositories;
using M03_Escola.Model;

namespace M03_Escola.DataBase.Repositories
{
    public class MateriaRepository : BaseRepository<Materia, int>, IMateriaRepository
    {
        public MateriaRepository(EscolaDBContexto contexto) : base(contexto)
        {
        }

        public List<Materia> ObterPorNome(string nome)
        {
            return _context.Set<Materia>().Where(x => x.Nome == nome).ToList();
        }
    }
}
