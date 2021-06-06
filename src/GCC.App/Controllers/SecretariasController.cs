using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GCC.App.ViewModels;
using AutoMapper;
using GCC.Business.Interfaces;
using GCC.Business.Modelos;
using GCC.App.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GCC.App.Controllers
{
    [Authorize]
    public class SecretariasController : Controller
    {
        private readonly ISecretariaRepository _secretariaRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public SecretariasController(ISecretariaRepository secretariaRepository, IMapper mapper, IUsuarioService usuarioService)
        {
            _secretariaRepository = secretariaRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [ClaimsAuthorize("Secretaria", "R")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SecretariaViewModel>>(await _secretariaRepository.ObterTodos()));
        }

        [ClaimsAuthorize("Secretaria", "R")]
        public async Task<IActionResult> Details(Guid id)
        {
            var secretariaViewModel = await ObterSecretariaPorId(id);

            if (secretariaViewModel == null)
            {
                return NotFound();
            }

            return View(secretariaViewModel);
        }

        [ClaimsAuthorize("Secretaria", "C")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Secretaria", "C")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SecretariaViewModel secretariaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(secretariaViewModel);
            }

            secretariaViewModel.CPF = secretariaViewModel.CPF.ApenasNumeros();
            secretariaViewModel.Telefone = secretariaViewModel.Telefone.ApenasNumeros();
            var secretaria = _mapper.Map<Secretaria>(secretariaViewModel);

            if ((await _secretariaRepository.Buscar(m => Equals(m.CPF, secretariaViewModel.CPF))).Any())
            {
                ModelState.AddModelError(string.Empty, "CPF já cadastrado!");
                return View(secretariaViewModel);
            }

            var usuarioIdentity = await _usuarioService.CadastrarSecretaria(secretariaViewModel.Email, secretariaViewModel.Email, secretariaViewModel.Senha);
            if (usuarioIdentity == null)
            {
                ModelState.AddModelError(string.Empty, "Email já em utilização!");
                return View(secretariaViewModel);
            }

            secretaria.UsuarioId = Guid.Parse(usuarioIdentity.Id);
            await _secretariaRepository.Adicionar(secretaria);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Secretaria", "U")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var secretariaViewModel = await ObterSecretariaPorId(id);

            if (secretariaViewModel == null)
            {
                return NotFound();
            }

            return View(secretariaViewModel);
        }

        [ClaimsAuthorize("Secretaria", "U")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SecretariaViewModel secretariaViewModel)
        {
            if (id != secretariaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(secretariaViewModel);
            }

            secretariaViewModel.CPF = secretariaViewModel.CPF.ApenasNumeros();
            secretariaViewModel.Telefone = secretariaViewModel.Telefone.ApenasNumeros();

            if (!await _usuarioService.AtualizeEmail(secretariaViewModel.UsuarioId, secretariaViewModel.Email))
            {
                ModelState.AddModelError(string.Empty, "Não foi possível atualizar o email.");
                return View(secretariaViewModel);
            }

            if (!await _usuarioService.AtualizeSenha(secretariaViewModel.UsuarioId, secretariaViewModel.SenhaAntiga, secretariaViewModel.Senha))
            {
                ModelState.AddModelError(string.Empty, "Não foi possível atualizar a senha.");
                return View(secretariaViewModel);
            }

            var secretaria = _mapper.Map<Secretaria>(secretariaViewModel);
            await _secretariaRepository.Atualizar(secretaria);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Secretaria", "D")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var secretariaViewModel = await ObterSecretariaPorId(id);

            if (secretariaViewModel == null)
            {
                return NotFound();
            }

            return View(secretariaViewModel);
        }

        [ClaimsAuthorize("Secretaria", "D")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var secretariaViewModel = await ObterSecretariaPorId(id);

            if (secretariaViewModel == null)
            {
                return NotFound();
            }

            await _secretariaRepository.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<SecretariaViewModel> ObterSecretariaPorId(Guid id)
        {
            var secretaria = _mapper.Map<SecretariaViewModel>(await _secretariaRepository.ObterPorId(id));
            return secretaria;
        }
    }
}
