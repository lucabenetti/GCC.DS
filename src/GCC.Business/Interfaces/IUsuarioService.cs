using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GCC.Business.Interfaces
{
    public interface IUsuarioService
    {
        Task<IdentityUser> CadastrarUsuario(string email, string username, string password);
        Task<IdentityUser> ObtenhaUsuario(Guid id);
        Task<bool> AtualizeSenha(Guid id, string oldPassword, string newPassword);
        Task<bool> AtualizeEmail(Guid id, string email);
    }
}
