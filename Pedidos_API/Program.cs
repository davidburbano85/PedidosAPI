using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pedidos_API;
using Pedidos_API.Infrastructura.BaseRespository;
using Pedidos_API.Infrastructura.ContractsOInterfaces;
using Pedidos_API.Infrastructura.ModelsPOCO;
using Pedidos_API.Infrastructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IConsumoRepositorio, ConsumoRepositorio>();
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddScoped<ICancionesRepositorio, CancionesRepositorio>();
builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio> ();
builder.Services.AddScoped<IMesasRepositorio, MesasRepositorio> ();
builder.Services.AddScoped<IStockRepositorio, StockRepositorio> ();
builder.Services.AddScoped<IProductosCartaRepositorio, ProductosCartaRepositorio> ();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));


});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options=>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200/");
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
