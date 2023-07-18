﻿using M03_Escola.DTO;
using M03_Escola.Util;

namespace M03_Escola.Model
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Permissao { get; set; }
        public string Senha { get; set; }
       
        public Usuario()
        {

        }
        public Usuario(UsuarioGetDTO usuario)
        {
            Nome = usuario.Nome;
            Login = usuario.Login;
            Permissao = usuario.Permissao;
            
        }
        public Usuario(UsuarioDTO usuario) : this((UsuarioGetDTO)usuario)
        {
            Senha = usuario.Senha;
        }

        public void Update(Usuario usuario)
        {
            Nome = usuario.Nome;
            Permissao = usuario.Permissao;
        }
    }
}
