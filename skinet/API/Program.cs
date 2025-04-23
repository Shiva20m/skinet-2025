using API.Middlewares;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>(opt=>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// cors - cross origion resource sharing
builder.Services.AddCors();
builder.Services.AddSingleton<IConnectionMultiplexer>(config=>
{
    var connString = builder.Configuration.GetConnectionString("Redis") ?? throw new Exception("cannot get rdis connection string");
    var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSingleton<ICartService, CartService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// use cors
app.UseCors(x=> x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http;//localhost:4200", "https://localhost:4200"));

// to add the seed data automatically into the database using entity framework
app.MapControllers();
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);

}
catch (Exception ex)
{
    Console.WriteLine(ex);
    
    throw;
}

app.Run();
