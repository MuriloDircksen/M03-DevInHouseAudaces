using M03_Escola.DTO;
using M03_Escola.Exceptions;
using M03_Escola.Interfaces.Services;
using M03_Escola.Model;
using M03_Escola.Util;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace M03_Escola.Services
{
    public class AutenticacaoServices : IAutenticacaoServices
    {
        private readonly IUsuarioService _usuarioService;

        private readonly string _chaveJwt;

        public AutenticacaoServices(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;

            _chaveJwt = configuration.GetSection("jwtTokenChave").Get<string>();
        }

        public string Autenticar(LoginDTO login)
        {
            var usuario = _usuarioService.ObterPorId(login.Usuario);
            if (usuario != null)
            {
                if( usuario.Senha == Criptografia.CriptografarSenha(login.Senha)) return GerarToken(usuario); ;

            }
            throw new LoginInvalidoException("Usuario ou senha Inválidos");

        }

        public string GerarToken(Usuario usuario)
        {           

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_chaveJwt);


            // Utilização de clains Dinamicamente 
            //var clains = new Dictionary<string, object>
            //       {
            //          { ClaimTypes.Name, usuario.Login },
            //          { "Nome", usuario.Nome },
            //          { "Interno", usuario.Interno.ToString() },
            //          { ClaimTypes.Role, usuario.Permissao },
            //       };

            //if (true)
            //{
            //    clains.Add("minhachave", "Meu valor");
            //}

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(),
            //    Claims = clains,
            //    Expires = DateTime.UtcNow.AddHours(4),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                      new Claim(ClaimTypes.Name, usuario.Login),
                      new Claim("Nome", usuario.Nome),
                      //new Claim("Interno", usuario.Interno.ToString()),
                      new Claim(ClaimTypes.Role, usuario.Permissao),
                  }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}
