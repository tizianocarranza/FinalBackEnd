using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalBackEnd.Models;
using AutoMapper;
using FinalBackEnd.DTOs;

namespace FinalBackEnd.Controllers
{
    public class ClasesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly FinalBackEndContext _context;

        public ClasesController(FinalBackEndContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Clases
        public async Task<IActionResult> Index()
        {
              return _context.Clases != null ? 
                          View(await _context.Clases.ToListAsync()) :
                          Problem("Entity set 'FinalBackEndContext.Clases'  is null.");
        }

        // GET: Clases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clases == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // GET: Clases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Dia")] ClaseDto claseDto)
        {
            if (ModelState.IsValid)
            {
                var clase = _mapper.Map<ClaseDto, Clase>(claseDto);
                _context.Add(clase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claseDto);
        }

        // GET: Clases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clases == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }
            return View(clase);
        }

        // POST: Clases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Dia")] ClaseDto claseDto)
        {
            if (id != claseDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var clase = _mapper.Map<ClaseDto, Clase>(claseDto);
                    _context.Update(clase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaseExists(claseDto.Id))
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
            return View(claseDto);
        }


        // GET: Clases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clases == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // POST: Clases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clases == null)
            {
                return Problem("Entity set 'FinalBackEndContext.Clases'  is null.");
            }
            var clase = await _context.Clases.FindAsync(id);
            if (clase != null)
            {
                _context.Clases.Remove(clase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaseExists(int id)
        {
          return (_context.Clases?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
