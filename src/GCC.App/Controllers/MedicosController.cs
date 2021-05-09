using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GCC.App.Data;
using GCC.App.ViewModels;
using GCC.Business.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using GCC.Business.Modelos;
using GCC.App.Extensions;

namespace GCC.App.Controllers
{
    public class MedicosController : Controller
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public MedicosController(IMedicoRepository medicoRepository, IMapper mapper, IUsuarioService usuarioService)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<MedicoViewModel>>(await _medicoRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var medicoViewModel = await ObterMedicoPorId(id);

            if (medicoViewModel == null)
            {
                return NotFound();
            }

            return View(medicoViewModel);
        }

        public IActionResult Create()
        {
            return View(new MedicoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicoViewModel medicoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(medicoViewModel);
            }

            medicoViewModel.CPF = medicoViewModel.CPF.ApenasNumeros();
            medicoViewModel.Telefone = medicoViewModel.Telefone.ApenasNumeros();
            var medico = _mapper.Map<Medico>(medicoViewModel);

            var usuarioIdentity = await _usuarioService.CadastrarUsuario(medicoViewModel.Email, medicoViewModel.Email, medicoViewModel.Senha);
            if (usuarioIdentity != null)
            {
                medico.UsuarioId = Guid.Parse(usuarioIdentity.Id);
                await _medicoRepository.Adicionar(medico);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var medicoViewModel = await ObterMedicoPorId(id);

            if (medicoViewModel == null)
            {
                return NotFound();
            }

            return View(medicoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MedicoViewModel medicoViewModel)
        {
            if (id != medicoViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(medicoViewModel);
            }

            medicoViewModel.CPF = medicoViewModel.CPF.ApenasNumeros();
            medicoViewModel.Telefone = medicoViewModel.Telefone.ApenasNumeros();

            if (!await _usuarioService.AtualizeEmail(medicoViewModel.UsuarioId, medicoViewModel.Email))
            {
                ModelState.AddModelError(string.Empty, "Não foi possível atualizar o email.");
                return View(medicoViewModel);
            }

            if (!await _usuarioService.AtualizeSenha(medicoViewModel.UsuarioId, medicoViewModel.SenhaAntiga, medicoViewModel.Senha))
            {
                ModelState.AddModelError(string.Empty, "Não foi possível atualizar a senha.");
                return View(medicoViewModel);
            }

            var medico = _mapper.Map<Medico>(medicoViewModel);
            await _medicoRepository.Atualizar(medico);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var medicoViewModel = await ObterMedicoPorId(id);

            if (medicoViewModel == null)
            {
                return NotFound();
            }

            return View(medicoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var medicoViewModel = await ObterMedicoPorId(id);

            if (medicoViewModel == null)
            {
                return NotFound();
            }

            await _medicoRepository.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<MedicoViewModel> ObterMedicoPorId(Guid id)
        {
            var medico = _mapper.Map<MedicoViewModel>(await _medicoRepository.ObtenhaMedico(id));
            var usuarioIdentity = await _usuarioService.ObtenhaUsuario(medico.UsuarioId);
            medico.Email = usuarioIdentity.Email;
            return medico;
        }
    }
}
