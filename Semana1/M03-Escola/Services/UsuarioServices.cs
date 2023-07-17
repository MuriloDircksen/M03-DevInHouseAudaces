using M03_Escola.Interfaces.Repositories;
using M03_Escola.Interfaces.Services;
using M03_Escola.Model;
using M03_Escola.Util;

namespace M03_Escola.Services
{
    public class UsuarioServices : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public Usuario Atualizar(Usuario usuario)
        {

            var usuarioDb = ObterPorId(usuario.Login);
            if (usuarioDb == null)
                throw new KeyNotFoundException("Usuario Não existe");

            usuarioDb.Update(usuario);
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuarioDb.Senha = Criptografia.CriptografarSenha(usuario.Senha);
            _usuarioRepository.Atualizar(usuarioDb);
            return usuario;
        }

        public Usuario Criar(Usuario usuario)
        {
            usuario.Senha = Criptografia.CriptografarSenha(usuario.Senha);
            return _usuarioRepository.Inserir(usuario);
        }

        public void Deletar(string login)
        {
            var usuarioDb = ObterPorId(login);
            if (usuarioDb == null)
                throw new KeyNotFoundException("Usuario Não existe");

            _usuarioRepository.Excluir(usuarioDb);
        }

        public List<Usuario> Obter()
        {
            return _usuarioRepository.ObterTodos();
        }

        public Usuario ObterPorId(string login)
        {
            return _usuarioRepository.ObterPorId(login);
        }
    }
}
