using M03_Escola.Model;

namespace M03_Escola.DTO
{
    public class UsuarioDTO : UsuarioGetDTO
    {
        public string Senha { get; set; }

        public UsuarioDTO()
        {

        }
        public UsuarioDTO(Usuario usuario) : base(usuario)
        {
            Senha = usuario.Senha;
        }
    }
}
