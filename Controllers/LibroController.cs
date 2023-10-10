using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial.Models;
using Parcial.ViewModels;

namespace Parcial.Controllers
{
    public class LibroController : Controller
    {
        private readonly LibreriaContext _context;

        public LibroController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: Libro  // Aca va a entrar el Filtro del Index Libro
        public async Task<IActionResult> Index(string Filter)
        {
            var query = from libro in _context.Libro select libro;
            //Aca me traigo todos los libros 
            query = query.Include(x=> x.Usuario);
            // el include nos trae los elementos de las relaciones
            if(!string.IsNullOrEmpty(Filter))
            {
                query = query.Where(x => x.Nombre.ToLower().Contains(Filter.ToLower()) || 
                                x.Domicilio.ToLower().Contains(Filter.ToLower()));
            }

            var libroList = await query.ToListAsync();//Me guardo los datos del toList en la var
            var libroListVM = new LibroListVM();//Instancio una var de tipo ListVM que le voy a pasar a la vista

            // Mapeamos la entidad con el view model para enviar a la vista
            foreach (var item in libroList)
            {
                libroListVM.Libros.Add(new LibroVM {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Domicilio = item.Domicilio,
                    Genero = item.Genero,
                    Año = item.Año,
                    Imagen = item.Imagen,
                    ClienteNombre = item.Usuario?.Nombre
                });
            }

            return View(libroListVM);
              //return View(await query.ToListAsync()); //ToListAsync me convierte la query en una lista y lo retorna a la vista
                         
        }

        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Libro == null)
            {
                return NotFound();
            }

            var libro = await _context.Libro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Año,Genero,Multilenguaje,Precio,Domicilio,Imagen,ClienteId")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Libro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Libro == null)
            {
                return NotFound();
            }

            var libro = await _context.Libro.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        // POST: Libro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Año,Genero,Multilenguaje,Precio,Domicilio,Imagen,ClienteId")] Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
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
            return View(libro);
        }

        // GET: Libro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Libro == null)
            {
                return NotFound();
            }

            var libro = await _context.Libro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Libro == null)
            {
                return Problem("Entity set 'LibreriaContext.Libro'  is null.");
            }
            var libro = await _context.Libro.FindAsync(id);
            if (libro != null)
            {
                _context.Libro.Remove(libro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
          return (_context.Libro?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
