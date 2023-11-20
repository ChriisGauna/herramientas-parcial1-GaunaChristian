using System.ComponentModel.DataAnnotations;

namespace Parcial.ViewModels;

public class LibroCreateVM 
{
    public int Id { get; set; }
    public string Nombre { get; set; } 
    public int Año { get; set; } 
    public string Genero { get; set; } 
    public bool Multilenguaje { get; set; } 
    public decimal Precio { get; set; } 
    public string Domicilio { get; set; } 
    public string Imagen { get; set; } 
    
    [Display(Name = "Cliente")]
    
    public List<int> UsuarioIds { get; set; }

    //public string? ClienteNombre { get; set; } //Agregamos otra variable distinta para mostrar en esta vista
}