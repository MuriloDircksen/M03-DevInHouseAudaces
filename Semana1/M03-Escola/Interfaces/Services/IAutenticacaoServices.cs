using M03_Escola.DTO;

namespace M03_Escola.Interfaces.Services
{
    public interface IAutenticacaoServices
    {
        bool Autenticar(LoginDTO login);
        string GerarToken(LoginDTO loginDTO);
    }
}
