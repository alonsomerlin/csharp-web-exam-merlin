using csharp_web_exam_api;
using csharp_web_exam_api.Context;
using csharp_web_exam_api.Repository;
using csharp_web_exam_api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Variable para la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("MvcContext");

//Registramos servicio de conexion
builder.Services.AddDbContext<AppDbContext>(options => 
                                            options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(MappingConfing));
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
