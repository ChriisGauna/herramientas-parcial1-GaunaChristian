using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parcial.Services;
using Microsoft.AspNetCore.Identity;


//using Data.LibreriaContext;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<LibreriaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LibreriaContext") ?? throw new 
    InvalidOperationException("Connection string 'LibreriaContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>
(options => options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<LibreriaContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ILibroService, LibroService>();// Inyectamos el servicio

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
