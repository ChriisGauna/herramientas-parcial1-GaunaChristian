namespace Parcial.Models;

public class Cliente //GameConsole
{
    public int Id { get; set; } // id 
    public string Nombre { get; set; } //Name
    public decimal Antiguedad { get; set; } // Price
    public string Domicilio { get; set; } // Company
    public virtual ICollection<Libro> Libros { get; set; } // Lista de juegos
}