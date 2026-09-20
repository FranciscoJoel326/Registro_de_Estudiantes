using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using Registro_de_Estudiantes.Components;
using Registro_de_Estudiantes.DAL;
using Registro_de_Estudiantes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr")));

builder.Services.AddScoped<EstudiantesServices>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredToast();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
