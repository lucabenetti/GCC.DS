using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GCC.App.Data;
using GCC.App.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Identity;
using GCC.Business.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using GCC.Business.Modelos;
using GCC.App.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GCC.App.Controllers
{
    [Authorize]
    public class PacientesController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public PacientesController(IPacienteRepository pacienteRepository, IMapper mapper, IUsuarioService usuarioService, IConsultaRepository consultaRepository)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
            _consultaRepository = consultaRepository;
        }

        [ClaimsAuthorize("Paciente", "R")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PacienteViewModel>>(await _pacienteRepository.ObterTodos()));
        }

        [ClaimsAuthorize("Paciente", "R")]
        public async Task<IActionResult> Details(Guid id)
        {
            var pacienteViewModel = await ObterPacientePorId(id);

            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

        [ClaimsAuthorize("Paciente", "C")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Paciente", "C")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacienteViewModel pacienteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(pacienteViewModel);
            }

            pacienteViewModel.CPF = pacienteViewModel.CPF.ApenasNumeros();
            pacienteViewModel.Telefone = pacienteViewModel.Telefone.ApenasNumeros();
            var paciente = _mapper.Map<Paciente>(pacienteViewModel);

            if ((await _pacienteRepository.Buscar(m => Equals(m.CPF, pacienteViewModel.CPF))).Any())
            {
                ModelState.AddModelError(string.Empty, "CPF já cadastrado!");
                return View(pacienteViewModel);
            }

            var usuarioIdentity = await _usuarioService.CadastrarPaciente(pacienteViewModel.Email, pacienteViewModel.Email, pacienteViewModel.Senha);
            if (usuarioIdentity == null) 
            {
                ModelState.AddModelError(string.Empty, "Email já em utilização!");
                return View(pacienteViewModel);
            }

            paciente.UsuarioId = Guid.Parse(usuarioIdentity.Id);
            await _pacienteRepository.Adicionar(paciente);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Paciente", "U")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var pacienteViewModel = await ObterPacientePorId(id);

            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

        [ClaimsAuthorize("Paciente", "U")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PacienteViewModel pacienteViewModel)
        {
            if (id != pacienteViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(pacienteViewModel);
            }

            pacienteViewModel.CPF = pacienteViewModel.CPF.ApenasNumeros();
            pacienteViewModel.Telefone = pacienteViewModel.Telefone.ApenasNumeros();

            if (!await _usuarioService.AtualizeEmail(pacienteViewModel.UsuarioId, pacienteViewModel.Email))
            {
                ModelState.AddModelError(string.Empty, "Não foi possível atualizar o email.");
                return View(pacienteViewModel);
            }

            if (!await _usuarioService.AtualizeSenha(pacienteViewModel.UsuarioId, pacienteViewModel.SenhaAntiga, pacienteViewModel.Senha))
            {
                ModelState.AddModelError(string.Empty, "Não foi possível atualizar a senha.");
                return View(pacienteViewModel);
            }

            var paciente = _mapper.Map<Paciente>(pacienteViewModel);
            await _pacienteRepository.Atualizar(paciente);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Paciente", "D")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pacienteViewModel = await ObterPacientePorId(id);

            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

        [ClaimsAuthorize("Paciente", "D")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pacienteViewModel = await ObterPacientePorId(id);

            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            if((await _consultaRepository.ObtenhaConsultasPaciente(id)).Any(c => c.Data > DateTime.Now))
            {
                ModelState.AddModelError(string.Empty, "Não é possível excluir pois existem consultas futuras");
                return View(pacienteViewModel);
            }

            await _pacienteRepository.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<PacienteViewModel> ObterPacientePorId(Guid id)
        {
            var paciente = _mapper.Map<PacienteViewModel>(await _pacienteRepository.ObtenhaPaciente(id));
            var usuarioIdentity = await _usuarioService.ObtenhaUsuario(paciente.UsuarioId);
            paciente.Email = usuarioIdentity.Email;
            return paciente;
        }
    }
}
