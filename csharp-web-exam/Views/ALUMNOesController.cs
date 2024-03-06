using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using csharp_web_exam_api.Context;
using csharp_web_exam_api.Models;

namespace csharp_web_exam.Views
{
    public class ALUMNOesController : Controller
    {
        private readonly AppDbContext _context;

        public ALUMNOesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ALUMNOes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ALUMNOs.Include(a => a.GENERO);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ALUMNOes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aLUMNO = await _context.ALUMNOs
                .Include(a => a.GENERO)
                .FirstOrDefaultAsync(m => m.ALUMNO_ID == id);
            if (aLUMNO == null)
            {
                return NotFound();
            }

            return View(aLUMNO);
        }

        // GET: ALUMNOes/Create
        public IActionResult Create()
        {
            ViewData["GENERO_ID"] = new SelectList(_context.GENEROs, "GENERO_ID", "GENERO_DESC");
            return View();
        }

        // POST: ALUMNOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ALUMNO_ID,NOMBRE,PATERNO,MATERNO,GENERO_ID,CORREO")] ALUMNO aLUMNO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aLUMNO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GENERO_ID"] = new SelectList(_context.GENEROs, "GENERO_ID", "GENERO_DESC", aLUMNO.GENERO_ID);
            return View(aLUMNO);
        }

        // GET: ALUMNOes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aLUMNO = await _context.ALUMNOs.FindAsync(id);
            if (aLUMNO == null)
            {
                return NotFound();
            }
            ViewData["GENERO_ID"] = new SelectList(_context.GENEROs, "GENERO_ID", "GENERO_DESC", aLUMNO.GENERO_ID);
            return View(aLUMNO);
        }

        // POST: ALUMNOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ALUMNO_ID,NOMBRE,PATERNO,MATERNO,GENERO_ID,CORREO")] ALUMNO aLUMNO)
        {
            if (id != aLUMNO.ALUMNO_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aLUMNO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ALUMNOExists(aLUMNO.ALUMNO_ID))
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
            ViewData["GENERO_ID"] = new SelectList(_context.GENEROs, "GENERO_ID", "GENERO_DESC", aLUMNO.GENERO_ID);
            return View(aLUMNO);
        }

        // GET: ALUMNOes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aLUMNO = await _context.ALUMNOs
                .Include(a => a.GENERO)
                .FirstOrDefaultAsync(m => m.ALUMNO_ID == id);
            if (aLUMNO == null)
            {
                return NotFound();
            }

            return View(aLUMNO);
        }

        // POST: ALUMNOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aLUMNO = await _context.ALUMNOs.FindAsync(id);
            if (aLUMNO != null)
            {
                _context.ALUMNOs.Remove(aLUMNO);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ALUMNOExists(int id)
        {
            return _context.ALUMNOs.Any(e => e.ALUMNO_ID == id);
        }
    }
}
