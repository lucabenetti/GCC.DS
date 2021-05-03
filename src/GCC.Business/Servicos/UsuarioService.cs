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
        public async Task<Guid> CadastrarUsuario(string email, string username, string password)
        {
            var usuario = new IdentityUser
            {
                Email = email,
                UserName  = username
            };

            await _userManager.CreateAsync(usuario, password);

            return Guid.Parse(usuario.Id);
        }
    }
}
