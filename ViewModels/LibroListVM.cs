namespace Parcial.ViewModels;

public class LibroListVM
{
    public List<LibroVM> Libros { get; set; } = new List<LibroVM>();
    public string? Filter { get; set; }
}