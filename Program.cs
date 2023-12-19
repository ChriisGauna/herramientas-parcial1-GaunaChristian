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
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<LibreriaContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ILibroService, LibroService>();// Inyectamos el servicio aca

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
app.UseAuthorization();// Agregado para que funcione la capa de seguridad del authorize
app.MapRazorPages();// 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Aca creamos el Manager y creamos los roles
using(var scope= app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles= new[] {"Admin", "User", "Manager"};

        foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

            }

}
/*
using(var scope= app.Services.CreateScope())
{
    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string email = "admin@admin.com";
        string password = "Test1234";

        if (await userManager.FindByEmailAsync(email)== null)
        {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        await userManager.CreateAsync (user, password);
            await userManager.AddToRoleAsync(user, "Admin");
        }

     

}*/
app.Run();
