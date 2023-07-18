using M03_Escola.Model;

namespace M03_Escola.DTO
{
    public class UsuarioGetDTO
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Permissao { get; set; }
       


        public UsuarioGetDTO()
        { }

        public UsuarioGetDTO(Usuario usuario)
        {
            Nome = usuario.Nome;
            Login = usuario.Login;
            Permissao = usuario.Permissao;
           
        }
    }
}
