using M03_Escola.Interfaces.Repositories;
using M03_Escola.Model;

namespace M03_Escola.DataBase.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario, string>, IUsuarioRepository
    {
        public UsuarioRepository(EscolaDBContexto contexto) : base(contexto)
        {
        }
    }
}
