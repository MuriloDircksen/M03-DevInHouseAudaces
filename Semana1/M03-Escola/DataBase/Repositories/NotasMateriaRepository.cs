using M03_Escola.Interfaces.Repositories;
using M03_Escola.Model;

namespace M03_Escola.DataBase.Repositories
{
    public class NotasMateriaRepository : BaseRepository<NotasMateria, int>, INotasMateriaRepository
    {
        public NotasMateriaRepository(EscolaDBContexto contexto) : base(contexto)
        {
        }

        public List<NotasMateria> ObterPorBoletimId(int boletimId)
        {
            return _context.Set<NotasMateria>().Where(x => x.BoletimId == boletimId).ToList();
        }
    }
}
