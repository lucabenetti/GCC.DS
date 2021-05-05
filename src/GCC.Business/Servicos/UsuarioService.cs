using GCC.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GCC.Business.Servicos
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UsuarioService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityUser> CadastrarUsuario(string email, string username, string password)
        {
            var usuario = new IdentityUser
            {
                Email = email,
                UserName  = username
            };

            var retorno = await _userManager.CreateAsync(usuario, password);

            if(!retorno.Succeeded)
            {
                return null;
            }

            return usuario;
        }
    }
}
