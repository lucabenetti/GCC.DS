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

namespace GCC.App.Controllers
{
    public class ExamesController : Controller
    {
        private readonly IExameRepository _exameRepository;
        private readonly IMapper _mapper;

        public ExamesController(IExameRepository exameRepository, IMapper mapper)
        {
            _exameRepository = exameRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ExameViewModel>>(await _exameRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var exameViewModel = await ObterExamePorId(id);

            if (exameViewModel == null)
            {
                return NotFound();
            }

            return View(exameViewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View(new ExameViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exame exameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(exameViewModel);
            }

            var exame = _mapper.Map<Exame>(exameViewModel);

            await _exameRepository.Adicionar(exame);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var exameViewModel = await ObterExamePorId(id);

            if (exameViewModel == null)
            {
                return NotFound();
            }

            return View(exameViewModel);
        }

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

            var exame = _mapper.Map<Exame>(exameViewModel);
            await _exameRepository.Atualizar(exame);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var exameViewModel = await ObterExamePorId(id);

            if (exameViewModel == null)
            {
                return NotFound();
            }

            return View(exameViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exameViewModel = await ObterExamePorId(id);

            if (exameViewModel == null)
            {
                return NotFound();
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
