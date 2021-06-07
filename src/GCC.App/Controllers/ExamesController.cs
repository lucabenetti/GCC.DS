using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GCC.App.ViewModels;
using GCC.Business.Interfaces;
using AutoMapper;
using GCC.Business.Modelos;
using Microsoft.AspNetCore.Authorization;
using GCC.App.Extensions;
using System.Linq;

namespace GCC.App.Controllers
{
    [Authorize]
    public class ExamesController : Controller
    {
        private readonly IExameRepository _exameRepository;
        private readonly IMapper _mapper;

        public ExamesController(IExameRepository exameRepository, IMapper mapper)
        {
            _exameRepository = exameRepository;
            _mapper = mapper;
        }

        [ClaimsAuthorize("Exame", "R")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ExameViewModel>>(await _exameRepository.ObterTodos()));
        }

        [ClaimsAuthorize("Exame", "R")]
        public async Task<IActionResult> Details(Guid id)
        {
            var exameViewModel = await ObterExamePorId(id);

            if (exameViewModel == null)
            {
                return NotFound();
            }

            return View(exameViewModel);
        }

        [ClaimsAuthorize("Exame", "C")]
        public async Task<IActionResult> Create()
        {
            return View(new ExameViewModel());
        }

        [ClaimsAuthorize("Exame", "C")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExameViewModel exameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(exameViewModel);
            }

            exameViewModel.Nome = exameViewModel.Nome.Trim().ToUpper();

            var jaCadastrado = await _exameRepository.JaCadastradoMesmoNome(exameViewModel.Nome);

            if(jaCadastrado)
            {
                ModelState.AddModelError(string.Empty, "Esse tipo de exame já foi cadastrado!");
                return View(exameViewModel);
            }

            var exame = _mapper.Map<Exame>(exameViewModel);

            await _exameRepository.Adicionar(exame);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Exame", "U")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var exameViewModel = await ObterExamePorId(id);

            if (exameViewModel == null)
            {
                return NotFound();
            }

            return View(exameViewModel);
        }

        [ClaimsAuthorize("Exame", "U")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ExameViewModel exameViewModel)
        {
            if (id != exameViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(exameViewModel);
            }

            exameViewModel.Nome = exameViewModel.Nome.Trim().ToUpper();

            var jaCadastrado = await _exameRepository.JaCadastradoMesmoNome(exameViewModel.Nome);

            if (jaCadastrado)
            {
                ModelState.AddModelError(string.Empty, "Esse tipo de exame já foi cadastrado!");
                return View(exameViewModel);
            }

            var exame = _mapper.Map<Exame>(exameViewModel);
            await _exameRepository.Atualizar(exame);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Exame", "D")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exameViewModel = await ObterExamePorId(id);

            if (exameViewModel == null)
            {
                return NotFound();
            }

            return View(exameViewModel);
        }

        [ClaimsAuthorize("Exame", "D")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exameViewModel = await ObterExamePorId(id);

            if (exameViewModel == null)
            {
                return NotFound();
            }

            var exame = await _exameRepository.ObterExame(id);

            if (exame.Consulta.Any())
            {
                ModelState.AddModelError(string.Empty, "Não é possível realizar exclusão pois existem consultas associadas a este exame!");
                return View(exameViewModel);
            }

            await _exameRepository.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<ExameViewModel> ObterExamePorId(Guid id)
        {
            return _mapper.Map<ExameViewModel>(await _exameRepository.ObterPorId(id));
        }
    }
}
