using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GCC.App.Data;
using GCC.App.ViewModels;

namespace GCC.App.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consulta
        public async Task<IActionResult> Index()
        {
            return View(await _context.ConsultaViewModel.ToListAsync());
        }

        // GET: Consulta/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultaViewModel = await _context.ConsultaViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultaViewModel == null)
            {
                return NotFound();
            }

            return View(consultaViewModel);
        }

        // GET: Consulta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consulta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Duracao,Realizada")] ConsultaViewModel consultaViewModel)
        {
            if (ModelState.IsValid)
            {
                consultaViewModel.Id = Guid.NewGuid();
                _context.Add(consultaViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultaViewModel);
        }

        // GET: Consulta/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultaViewModel = await _context.ConsultaViewModel.FindAsync(id);
            if (consultaViewModel == null)
            {
                return NotFound();
            }
            return View(consultaViewModel);
        }

        // POST: Consulta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Data,Duracao,Realizada")] ConsultaViewModel consultaViewModel)
        {
            if (id != consultaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultaViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaViewModelExists(consultaViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consultaViewModel);
        }

        // GET: Consulta/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultaViewModel = await _context.ConsultaViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultaViewModel == null)
            {
                return NotFound();
            }

            return View(consultaViewModel);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var consultaViewModel = await _context.ConsultaViewModel.FindAsync(id);
            _context.ConsultaViewModel.Remove(consultaViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaViewModelExists(Guid id)
        {
            return _context.ConsultaViewModel.Any(e => e.Id == id);
        }
    }
}
