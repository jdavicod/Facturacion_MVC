using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Facturacion_MVC.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Servicios MVC
builder.Services.AddControllersWithViews();

// Configuración según entorno
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                         .AddEnvironmentVariables();
}
else
{
    builder.Configuration.AddEnvironmentVariables();
}

// Construir cadena de conexión
var baseConnection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")
                 ?? Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING")
                 ?? string.Empty;

var dbPassword = Environment.GetEnvironmentVariable("AZURE_SQL_PASSWORD")
                 ?? builder.Configuration["AZURE_SQL_PASSWORD"];

var sqlBuilder = new SqlConnectionStringBuilder(baseConnection);
if (!string.IsNullOrWhiteSpace(dbPassword))
{
    sqlBuilder.Password = dbPassword;
}

var connection = sqlBuilder.ConnectionString;

// Registrar DbContext
builder.Services.AddDbContext<FacturacionContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

// Middleware esencial
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Ruta principal MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Principal}/{action=Index}/{id?}");

// Comprobación de conexión
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FacturacionContext>();

    if (dbContext.Database.CanConnect())
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Conexión exitosa con la base de datos DB-facturacion-g7.");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
    }
    Console.ResetColor();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Error al intentar conectarse:");
    Console.WriteLine(ex);
    Console.ResetColor();
}

app.Run();
