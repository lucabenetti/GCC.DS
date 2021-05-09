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
using GCC.Business.Modelos;

namespace GCC.App.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;

        public ConsultasController(IConsultaRepository consultaRepository, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ConsultaViewModel>>(await _consultaRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var consultaViewModel = await ObterConsultaPorId(id);

            if (consultaViewModel == null)
            {
                return NotFound();
            }

            return View(consultaViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultaViewModel consultaViewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    consultaViewModel.Id = Guid.NewGuid();
            //    _context.Add(consultaViewModel);
            //    await _context.SaveChangesAsync();
            //    return View(consultaViewModel);
            //}

            var consulta = _mapper.Map<Consulta>(consultaViewModel);
            await _consultaRepository.Adicionar(consulta);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var consultaViewModel = await ObterConsultaPorId(id);

            if (consultaViewModel == null)
            {
                return NotFound();
            }

            return View(consultaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ConsultaViewModel consultaViewModel)
        {
            if (id != consultaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(consultaViewModel);
            }

            var consulta = _mapper.Map<Consulta>(consultaViewModel);
            await _consultaRepository.Atualizar(consulta);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var consultaViewModel = await ObterConsultaPorId(id);

            if (consultaViewModel == null)
            {
                return NotFound();
            }

            return View(consultaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var consultaViewModel = await ObterConsultaPorId(id);

            if (consultaViewModel == null)
            {
                return NotFound();
            }

            await _consultaRepository.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<ConsultaViewModel> ObterConsultaPorId(Guid id)
        {
            return _mapper.Map<ConsultaViewModel>(await _consultaRepository.ObtenhaConsulta(id));
        }
    }
}
