namespace Parcial.Models;

public class Cliente 
{
    public int Id { get; set; } 
    public string Nombre { get; set; } 
    public decimal Antiguedad { get; set; } 
    public string Domicilio { get; set; } 
    public virtual ICollection<Libro> Libros { get; set; } 
}