using System.ComponentModel.DataAnnotations;

namespace Parcial.ViewModels;

public class OperacionCreateVM
{
    public int Factura { get; set; }
    public DateTime Fecha { get; set; }
    public int Cantidad { get; set; }
    public int LibroId { get; set; }
}