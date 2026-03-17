using GCook.Data;
using GCook.Models;
using GCook.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Serviço de conexão ao Contexto.
string conexao = builder.Configuration.GetConnectionString("Conexao");
builder.Services.AddDbContext<AppDbContext>(
    Options => Options.UseSqlServer(conexao)
);

//Configuração do serviço de Identity
builder.Services.AddIdentity<Usuario, IdentityRole>(
    Options =>
    {
        Options.SignIn.RequireConfirmedEmail = false;
        Options.User.RequireUniqueEmail = true;
        Options.Lockout.MaxFailedAccessAttempts = 5;
    }
) 
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();    

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

//Código para garantir a existencia do banco de dados
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
