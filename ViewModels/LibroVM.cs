using System.ComponentModel.DataAnnotations;

namespace Parcial.ViewModels;

public class LibroVM 
{
    public int Id { get; set; }
    public string Nombre { get; set; } //Name
    public int Año { get; set; } //Release
    public string Genero { get; set; } //Gender
    public string Domicilio { get; set; } //
    public string Imagen { get; set; } //Image
    
    [Display(Name = "Cliente")]
    
    public string? ClienteNombre { get; set; } //Agregamos otra variable distinta para mostrar en esta vista
}