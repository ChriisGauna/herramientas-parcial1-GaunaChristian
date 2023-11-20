namespace Parcial.Models;


public class Libro 
{
    public int Id { get; set; }
    public string Nombre { get; set; } 
    public int Año { get; set; } 
    public string Genero { get; set; } 
    public bool Multilenguaje { get; set; } 
    public decimal Precio { get; set; } 
    public string Domicilio { get; set; } 
    public string Imagen { get; set; } 
    public int Stock { get; set; } 
    
    //public int? ClienteId { get; set; } //Foreing Key

    public virtual ICollection<Cliente> Usuarios { get; set; } //Ahora esto va a ser una lista de usuarios
    //virtual te permite que una propiedad y método pueda ser sobreescrita por una clase que herede de ella.
}