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
    public class MedicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {
            return View(await _context.MedicoViewModel.ToListAsync());
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoViewModel = await _context.MedicoViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicoViewModel == null)
            {
                return NotFound();
            }

            return View(medicoViewModel);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Nome,CPF,Sexo,Endereco,Telefone,DataNascimento")] MedicoViewModel medicoViewModel)
        {
            if (ModelState.IsValid)
            {
                medicoViewModel.Id = Guid.NewGuid();
                _context.Add(medicoViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicoViewModel);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoViewModel = await _context.MedicoViewModel.FindAsync(id);
            if (medicoViewModel == null)
            {
                return NotFound();
            }
            return View(medicoViewModel);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UsuarioId,Nome,CPF,Sexo,Endereco,Telefone,DataNascimento")] MedicoViewModel medicoViewModel)
        {
            if (id != medicoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicoViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoViewModelExists(medicoViewModel.Id))
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
            return View(medicoViewModel);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoViewModel = await _context.MedicoViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicoViewModel == null)
            {
                return NotFound();
            }

            return View(medicoViewModel);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var medicoViewModel = await _context.MedicoViewModel.FindAsync(id);
            _context.MedicoViewModel.Remove(medicoViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoViewModelExists(Guid id)
        {
            return _context.MedicoViewModel.Any(e => e.Id == id);
        }
    }
}
