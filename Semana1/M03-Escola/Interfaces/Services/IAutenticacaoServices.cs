using M03_Escola.DTO;
using M03_Escola.Model;

namespace M03_Escola.Interfaces.Services
{
    public interface IAutenticacaoServices
    {
        string Autenticar(LoginDTO login);
        string GerarToken(Usuario usuario);
    }
}
