using Parcial.Models;

namespace Parcial.Services;

public interface ILibroService
{
    Task<List<Libro>> GetAll(string filter);
    Task Update(Libro libro);
    Task Delete(int id);
    Task Create(Libro libro);
    Task<Libro> GetById(int? id);
    Task<List<Cliente>> GetAllClientes();
    Task<string> Compra (Operacion operacion);
    Task<string> Venta (Operacion operacion);
}