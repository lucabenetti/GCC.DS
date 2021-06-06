using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GCC.App.ViewModels;
using GCC.Business.Interfaces;
using AutoMapper;
using GCC.Business.Modelos;
using GCC.App.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GCC.App.Controllers
{
    [Authorize]
    public class MedicosController : Controller
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public MedicosController(IMedicoRepository medicoRepository, IMapper mapper, IUsuarioService usuarioService, IConsultaRepository consultaRepository)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
            _consultaRepository = consultaRepository;
        }

        [ClaimsAuthorize("Medico", "R")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<MedicoViewModel>>(await _medicoRepository.ObterTodos()));
        }

        [ClaimsAuthorize("Medico", "R")]
        public async Task<IActionResult> Details(Guid id)
        {
            var medicoViewModel = await ObterMedicoPorId(id);

            if (medicoViewModel == null)
            {
                return NotFound();
            }

            return View(medicoViewModel);
        }

        [ClaimsAuthorize("Medico", "C")]
        public IActionResult Create()
        {
            return View(new MedicoViewModel());
        }

        [ClaimsAuthorize("Medico", "C")]
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

            if((await _medicoRepository.Buscar(m => Equals(m.CPF, medicoViewModel.CPF))).Any())
            {
                ModelState.AddModelError(string.Empty, "CPF já cadastrado!");
            }

            if ((await _medicoRepository.ObtenhaMedicoPorCRM(medico.CRM) != null))
            {
                ModelState.AddModelError(string.Empty, "CRM já cadastrado!");
            }

            if(ModelState.ErrorCount > 0)
            {
                return View(medicoViewModel);
            }

            var usuarioIdentity = await _usuarioService.CadastrarMedico(medicoViewModel.Email, medicoViewModel.Email, medicoViewModel.Senha);
            if (usuarioIdentity == null)
            {
                ModelState.AddModelError(string.Empty, "Email já em utilização!");
                return View(medicoViewModel);
            }

            medico.UsuarioId = Guid.Parse(usuarioIdentity.Id);
            await _medicoRepository.Adicionar(medico);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Medico", "U")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var medicoViewModel = await ObterMedicoPorId(id);

            if (medicoViewModel == null)
            {
                return NotFound();
            }

            return View(medicoViewModel);
        }

        [ClaimsAuthorize("Medico", "U")]
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

            if ((await _consultaRepository.ObtenhaConsultasMedico(id)).Any(c => c.Data > DateTime.Now))
            {
                ModelState.AddModelError(string.Empty, "Não é possível editar pois existem consultas futuras na jornada de trabalho alterada.");
                return View(medicoViewModel);
            }

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

        [ClaimsAuthorize("Medico", "D")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var medicoViewModel = await ObterMedicoPorId(id);

            if (medicoViewModel == null)
            {
                return NotFound();
            }

            return View(medicoViewModel);
        }

        [ClaimsAuthorize("Medico", "D")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var medicoViewModel = await ObterMedicoPorId(id);

            if (medicoViewModel == null)
            {
                return NotFound();
            }

            if ((await _consultaRepository.ObtenhaConsultasMedico(id)).Any(c => c.Data > DateTime.Now))
            {
                ModelState.AddModelError(string.Empty, "Não é possível excluir pois existem consultas futuras");
                return View(medicoViewModel);
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
