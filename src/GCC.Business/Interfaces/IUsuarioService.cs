using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GCC.Business.Interfaces
{
    public interface IUsuarioService
    {
        Task<IdentityUser> CadastrarUsuario(string email, string username, string password);
    }
}
