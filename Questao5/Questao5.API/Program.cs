using Questao5.API.Filters;
using Questao5.CrossCutting.AppDependencies;
using Questao5.Infrastructure.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registro dos serviços
builder.Services.AddInfrastructure(builder.Configuration);

// Registra o filtro de exceção personalizado
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new CustomExceptionFilter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();
