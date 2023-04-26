using Microsoft.EntityFrameworkCore;
using API.Models.Context;
using API.Authorization;
using API.Helpers;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

string conexion = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseSqlServer(conexion));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddCors();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "admin",
        policy => policy.RequireClaim(ClaimTypes.Role, "admin")
    );
    options.AddPolicy(
       "user",
       policy => policy.RequireClaim(ClaimTypes.Role, "user")
   );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
