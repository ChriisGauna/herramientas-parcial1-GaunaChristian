using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial.Models;
using Parcial.Services;
using Parcial.ViewModels;

namespace Parcial.Controllers
{
    [Authorize]
    public class LibroController : Controller
    {
        private readonly ILibroService _Libroservicio;

        public LibroController(ILibroService libroService) //El constructor inyecta el servicio tambien ahora
        {
            _Libroservicio = libroService;
        }

        // GET: Libro  // Aca va a entrar el Filtro del Index Libro
        public async Task<IActionResult> Index(string Filter)
        {
          
            var libroListVM = new LibroListVM();//Instancio una var de tipo ListVM que le voy a pasar a la vista
            var libroList = await _Libroservicio.GetAll(Filter);
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
                    Stock = item.Stock
               //     ClienteNombre = item.Usuario?.Nombre
                });
            }

            return View(libroListVM);
                         
        }


        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var libro = await _Libroservicio.GetById(id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libro/Create
        public async Task<IActionResult> Create()
        {
            var clienteList = await _Libroservicio.GetAllClientes(); //Traigo los clientes por el servicio
            if (clienteList == null) clienteList = new List<Cliente>();//para que no rompa si viene null
            ViewData["Clientes"] = new SelectList(clienteList, "Id", "Nombre"); //Guardo el id y Nombre para pintarlo en la vista
            return View();
        }

        // POST: Libro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Año,Genero,Multilenguaje,Precio,Domicilio,Imagen,UsuarioIds")] LibroCreateVM libro)
        {
            if (!ModelState.IsValid)
            {
                var clienteList = await _Libroservicio.GetAllClientes(); //Me los traigo con el servicio
                var clientefilterList = clienteList.Where(x=> libro.UsuarioIds.Contains(x.Id)).ToList();
                var newLibro = new Libro{
                    Nombre = libro.Nombre,
                    Año = libro.Año,
                    Genero = libro.Genero,
                    Multilenguaje = libro.Multilenguaje,// MAPEO TODOS LOS DATOS
                    Precio = libro.Precio,
                    Domicilio = libro.Domicilio,
                    Imagen = libro.Imagen,
                    Usuarios = clientefilterList,

                };
                await _Libroservicio.Create(newLibro);
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Libro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
          

            var libro = await _Libroservicio.GetById(id);
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

            if (!ModelState.IsValid)//
            {
                try
                {
                    await _Libroservicio.Update(libro);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_Libroservicio.GetById(id) == null)
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
            var libro = await _Libroservicio.GetById(id);
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
           await _Libroservicio.Delete(id);
            return RedirectToAction(nameof(Index));
        }




         // GET: Libro/Compra
        public async Task<IActionResult> Compra (int id)
        {
            var libro = await _Libroservicio.GetById(id);//me traigo los libros
            if (libro == null)
            {
                return NotFound();
            }

            ViewData["Libro"] = libro;

            return View();
        }

        // POST: Libro/Compra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Compra ([Bind("LibroId,Fecha,Cantidad,Factura")] OperacionCreateVM compra)
        {
            if (ModelState.IsValid)
            {
                var nuevaCompra = new Operacion {
                    LibroId = compra.LibroId,
                    Cantidad = compra.Cantidad,
                    Factura = compra.Factura,
                    Fecha = compra.Fecha,
                    TipoMov = Utils.TipoMovimiento.compra
                };
                await _Libroservicio.Compra(nuevaCompra);
                return RedirectToAction(nameof(Index));
            }
            return View(compra);
        }

        // GET: Libro/Venta
        public async Task<IActionResult> Venta(int id)
        {
            var libro = await _Libroservicio.GetById(id);
            if (libro == null)
            {
                return NotFound();
            }

            ViewData["Libro"] = libro;

            return View();
        }

        // POST:  Libro/Venta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Venta([Bind("LibroId,Fecha,Cantidad,Factura")] OperacionCreateVM venta)
        {
            if (ModelState.IsValid)
            {
                var newVenta = new Operacion {
                    LibroId = venta.LibroId,
                    Cantidad = venta.Cantidad,
                    Factura = venta.Factura,
                    Fecha = venta.Fecha,
                    TipoMov = Utils.TipoMovimiento.venta
                };
                var response = await _Libroservicio.Venta(newVenta);

                if (string.IsNullOrEmpty(response))
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ErrorMsg"] = response;
            }
            return View(venta);
        }

    }
}
