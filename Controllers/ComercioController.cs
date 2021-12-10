using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order2Go.Data;
using Order2Go.Models;
using Microsoft.EntityFrameworkCore;

namespace Order2Go.Controllers
{
    public class ComercioController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ComercioController(ApplicationDbContext context )
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Reporte()
        {
            return View(await _context.Comercios.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comercios.ToListAsync());
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var BuscarComercio = _context.Comercios.Find(id);
            if (BuscarComercio == null)
            {
                return NotFound();
            }
            return View(BuscarComercio);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comercio comercio)
        {
            if (ModelState.IsValid)
            {  
                _context.Add(comercio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var BuscarComercio = _context.Comercios.Find(id);
            if (BuscarComercio == null)
            {
                return NotFound();
            }
            return View(BuscarComercio);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComercio(int? id)
        {
            var BuscarComercio = await _context.Comercios.FindAsync(id);
            
            if (BuscarComercio == null)
            {
                return View();
            }
           _context.Comercios.Remove(BuscarComercio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Comercio comercio)
        {
            if (ModelState.IsValid)
            {
                                              
                    _context.Update(comercio);
                    await _context.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index));
            }
            return View(comercio);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var BuscarComercio = _context.Comercios.Find(id);
            if (BuscarComercio == null)
            {
                return NotFound();
            }
            return View(BuscarComercio);
        }


    }
}
