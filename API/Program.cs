using Microsoft.EntityFrameworkCore;
using API.Models;
var builder = WebApplication.CreateBuilder(args);

string conexion = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseSqlServer(conexion));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
