using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProyectoEscuela.Server.DTOs.Alumno;
using ProyectoEscuela.Server.DTOs.Asistencia;
using ProyectoEscuela.Server.DTOs.Calificacion;
using ProyectoEscuela.Server.DTOs.Maestro;
using ProyectoEscuela.Server.DTOs.Materia;
using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Models;
using ProyectoEscuela.Server.Repository;
using ProyectoEscuela.Server.Services;
using ProyectoEscuela.Server.Validations.Alumno;
using ProyectoEscuela.Server.Validations.Asistencia;
using ProyectoEscuela.Server.Validations.Calificacion;
using ProyectoEscuela.Server.Validations.Maestro;
using ProyectoEscuela.Server.Validations.Materia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region ConnectionString
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion
#region DependencyInjection
#region Repository
builder.Services.AddTransient<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddTransient<IMateriaRepository, MateriaRepository>();
builder.Services.AddTransient<IMaestroRepository, MaestroRepository>();
builder.Services.AddTransient<ICalificacionRepository, CalificacionRepository>();
builder.Services.AddTransient<IAsistenciaRepository, AsistenciaRepository>();
#endregion
#region Service
builder.Services.AddTransient<IAlumnoService, AlumnoService>();
builder.Services.AddTransient<IMateriaService, MateriaService>();
builder.Services.AddTransient<IMaestroService, MaestroService>();
builder.Services.AddTransient<ICalificacionService, CalificacionService>();
builder.Services.AddTransient<IAsistenciaService, AsistenciaService>();
#endregion
#region Validation
builder.Services.AddScoped<IValidator<AlumnoInsertDto>, AlumnoInsertDtoValidation>();
builder.Services.AddScoped<IValidator<AlumnoUpdateDto>, AlumnoUpdateDtoValidation>();
builder.Services.AddScoped<IValidator<MateriaInsertDto>, MateriaInsertDtoValidation>();
builder.Services.AddScoped<IValidator<MateriaUpdateDto>, MateriaUpdateDtoValidation>();
builder.Services.AddScoped<IValidator<MaestroInsertDto>, MaestroInsertDtoValidation>();
builder.Services.AddScoped<IValidator<MaestroUpdateDto>, MaestroUpdateDtoValidation>();
builder.Services.AddScoped<IValidator<CalificacionInsertDto>, CalificacionInsertDtoValidator>();
builder.Services.AddScoped<IValidator<CalificacionUpdateDto>, CalificacionUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<AsistenciaInsertDto>, AsistenciaInsertDtoValidation>();
builder.Services.AddScoped<IValidator<AsistenciaUpdateDto>, AsistenciaUpdateDtoValidation>();
#endregion
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy => policy
            .WithOrigins("http://localhost:4200") 
            .AllowAnyHeader()
            .AllowAnyMethod());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAngularDev");
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
