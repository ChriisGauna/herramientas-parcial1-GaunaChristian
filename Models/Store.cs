namespace Parcial.Models;
public class Store 
{
    public int Id { get; set; } 
    public string email { get; set; } 
    public string street { get; set; } 
    public int? ProviderId { get; set; }
    public virtual Store ProviderStore { get; set; } 
}