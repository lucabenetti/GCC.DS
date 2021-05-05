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

namespace GCC.App.Controllers
{
    public class PacientesController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public PacientesController(IPacienteRepository pacienteRepository, IMapper mapper, IUsuarioService usuarioService)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PacienteViewModel>>(await _pacienteRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var pacienteViewModel = await ObterPacientePorId(id);

            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacienteViewModel pacienteViewModel)
        {
            //if (!ModelState.IsValid)
            //{
                //return View(pacienteViewModel);
            //}

            var usuarioIdentity = await _usuarioService.CadastrarUsuario("teste2", "luca2@mail.com", "#snKBCD178");

            if (usuarioIdentity != null) { }

            var paciente = _mapper.Map<Paciente>(pacienteViewModel);
            paciente.UsuarioId = Guid.Parse(usuarioIdentity.Id);
            await _pacienteRepository.Adicionar(paciente);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var pacienteViewModel = await ObterPacientePorId(id);

            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

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

            var paciente = _mapper.Map<Paciente>(pacienteViewModel);
            await _pacienteRepository.Atualizar(paciente);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var pacienteViewModel = ObterPacientePorId(id);

            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pacienteViewModel = await ObterPacientePorId(id);

            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            await _pacienteRepository.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<PacienteViewModel> ObterPacientePorId(Guid id)
        {
            return _mapper.Map<PacienteViewModel>(await _pacienteRepository.ObtenhaPaciente(id));
        }
    }
}
