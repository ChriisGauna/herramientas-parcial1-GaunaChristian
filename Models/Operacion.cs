using Parcial.Utils;

namespace Parcial.Models;

public class Operacion 
{
    public int Id { get; set; }
    public int Factura { get; set; }
    public DateTime Fecha { get; set; }
    public TipoMovimiento TipoMov { get; set; }
    public int Cantidad { get; set; }
    //public bool EnvioaDomicilio { get; set; }
    public int LibroId { get; set; }
    public virtual Libro Libro { get; set; }
}