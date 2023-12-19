namespace Parcial.Models;
public class Store  // Relacion uno a uno
{
    public int Id { get; set; } 
    public string email { get; set; } 
    public string street { get; set; } 
    public int? ProviderId { get; set; } //Una tienda tiene un proveedor
    public virtual Store ProviderStore { get; set; } 
}