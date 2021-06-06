using GCC.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        public async Task<IdentityUser> CadastrarMedico(string email, string username, string password)
        {
            var usuarioIdentity = await CadastrarUsuario(email, username, password);

            var claims = new List<Claim>() {
                new Claim("Consulta", "R, U"),
                new Claim("Exame", "C,R,U,D"),
                new Claim("Tipo", "M")
            };

            await _userManager.AddClaimsAsync(usuarioIdentity, claims);

            return usuarioIdentity;
        }

        public async Task<IdentityUser> CadastrarPaciente(string email, string username, string password)
        {
            var usuarioIdentity = await CadastrarUsuario(email, username, password);

            var claims = new List<Claim>() {
                new Claim("Consulta", "R"),
                new Claim("Tipo", "P")
            };

            await _userManager.AddClaimsAsync(usuarioIdentity, claims);

            return usuarioIdentity;
        }

        public async Task<IdentityUser> CadastrarSecretaria(string email, string username, string password)
        {
            var usuarioIdentity = await CadastrarUsuario(email, username, password);

            var claims = new List<Claim>() {
                new Claim("Consulta", "C,R,U,D"),
                new Claim("Medico", "C,R,U,D"),
                new Claim("Paciente", "C,R,U,D"),
                new Claim("Exame", "R"),
                new Claim("Tipo", "S")
            };

            await _userManager.AddClaimsAsync(usuarioIdentity, claims);

            return usuarioIdentity;
        }

        public async Task<IdentityUser> CadastrarUsuario(string email, string username, string password)
        {
            var usuario = new IdentityUser
            {
                Email = email,
                UserName = username
            };

            var retorno = await _userManager.CreateAsync(usuario, password);

            if(!retorno.Succeeded)
            {
                return null;
            }

            return usuario;
        }

        public async Task<IdentityUser> ObtenhaUsuario(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString()); ;
        }

        public async Task<bool> AtualizeSenha(Guid id,string oldPassword, string newPassword)
        {
            var usuario = await ObtenhaUsuario(id);

            return (await _userManager.ChangePasswordAsync(usuario, oldPassword, newPassword)).Succeeded;
        }

        public async Task<bool> AtualizeEmail(Guid id, string email)
        {
            var usuario = await ObtenhaUsuario(id);
            usuario.Email = email;
            usuario.UserName = email;
            return (await _userManager.UpdateAsync(usuario)).Succeeded;
        }
    }
}
