using Microsoft.EntityFrameworkCore;
using Parcial.Models;

namespace Parcial.Services;

public class LibroService : ILibroService
{
    private readonly LibreriaContext _context;

     public LibroService(LibreriaContext context)
        {
            _context = context;
        }

    public async Task Create(Libro libro)
    {
        _context.Add(libro);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var libro = await _context.Libro.FindAsync(id);
        if (libro!= null)
        {
            _context.Libro.Remove(libro);
        }
        
        await _context.SaveChangesAsync();;
    }

    public async Task<List<Libro>> GetAll(string Filter)
    {
        var query = from libro in _context.Libro select libro;
            //Aca me traigo todos los libros con linQ
          //  query = query.Include(x=> x.Usuario);
            // el include nos trae los elementos de las relaciones
            if(!string.IsNullOrEmpty(Filter))
            {
                query = query
                    .Where(x => x.Nombre.ToLower().Contains(Filter.ToLower()) || 
                                x.Domicilio.ToLower().Contains(Filter.ToLower()));
            }

            return await query.ToListAsync();//Me guardo los datos del toList en la var
    }

    public async Task<List<Cliente>> GetAllClientes()
    {
        return await _context.Cliente.ToListAsync();
    }

    public async Task<Libro?> GetById(int? id)
    {
         if (id == null || _context.Libro == null)
            {
                return null;
            }

           return await _context.Libro
                .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task Update(Libro libro)
    {
        _context.Update(libro);
        await _context.SaveChangesAsync();
    }

    public async Task<string> Compra(Operacion operacion)
    {
        return await CrearOperacion(operacion);
    }

    public async Task<string> Venta(Operacion operacion)
    {
        return await CrearOperacion(operacion);
    }

    

    private async Task<string> CrearOperacion(Operacion operacion)
    {
        var stock = operacion.Cantidad;
        var libro = await _context.Libro.FirstOrDefaultAsync(m => m.Id == operacion.LibroId);
        if (operacion.TipoMov == Utils.TipoMovimiento.venta)
        {
            stock*= -1;
            if ((libro.Stock + stock) < 0){
                return "No hay stock para " + libro.Nombre;
            }
        }

        if (libro is null)
        {
            return "El libro no existe";
        }  

        libro.Stock += stock;
        _context.Update(libro);
        _context.Add(operacion);
        await _context.SaveChangesAsync();

        return string.Empty;
    }
}