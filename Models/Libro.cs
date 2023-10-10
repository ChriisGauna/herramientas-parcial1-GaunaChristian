namespace Parcial.Models;


public class Libro //Game
{
    public int Id { get; set; }
    public string Nombre { get; set; } //Name
    public int Año { get; set; } //Release
    public string Genero { get; set; } //Gender
    public bool Multilenguaje { get; set; } //IsMultiplayer
    public decimal Precio { get; set; } //Price
    public string Domicilio { get; set; } //
    public string Imagen { get; set; } //Image
    public int? ClienteId { get; set; } //Foreing Key

    public virtual Cliente Usuario { get; set; }
}