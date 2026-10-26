using Application.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Authorization;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() }
    });
});

// Add Entity Framework Context
builder.Services.AddDbContext<TPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TPIConnection")));

// Add Dependency Injection
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();
builder.Services.AddScoped<IComisionRepository, ComisionRepository>();
builder.Services.AddScoped<IComisionService, ComisionService>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IAlumnoInscripcionRepository, AlumnoInscripcionRepository>();
builder.Services.AddScoped<IAlumnoInscripcionService, AlumnoInscripcionService>();
builder.Services.AddScoped<IDocenteCursoRepository, DocenteCursoRepository>();
builder.Services.AddScoped<IDocenteCursoService, DocenteCursoService>();
builder.Services.AddScoped<IModuloUsuarioRepository, ModuloUsuarioRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthorizationHandler, PermisoAuthorizationHandler>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Autenticación JWT
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Políticas de autorización por módulo + acción
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Personas.Alta", p => p.Requirements.Add(new PermisoRequirement("Personas", "Alta")))
    .AddPolicy("Personas.Baja", p => p.Requirements.Add(new PermisoRequirement("Personas", "Baja")))
    .AddPolicy("Personas.Modificar", p => p.Requirements.Add(new PermisoRequirement("Personas", "Modificar")))
    .AddPolicy("Personas.Consultar", p => p.Requirements.Add(new PermisoRequirement("Personas", "Consultar")))
    .AddPolicy("Materias.Alta", p => p.Requirements.Add(new PermisoRequirement("Materias", "Alta")))
    .AddPolicy("Materias.Baja", p => p.Requirements.Add(new PermisoRequirement("Materias", "Baja")))
    .AddPolicy("Materias.Modificar", p => p.Requirements.Add(new PermisoRequirement("Materias", "Modificar")))
    .AddPolicy("Materias.Consultar", p => p.Requirements.Add(new PermisoRequirement("Materias", "Consultar")))
    .AddPolicy("Planes.Alta", p => p.Requirements.Add(new PermisoRequirement("Planes", "Alta")))
    .AddPolicy("Planes.Baja", p => p.Requirements.Add(new PermisoRequirement("Planes", "Baja")))
    .AddPolicy("Planes.Modificar", p => p.Requirements.Add(new PermisoRequirement("Planes", "Modificar")))
    .AddPolicy("Planes.Consultar", p => p.Requirements.Add(new PermisoRequirement("Planes", "Consultar")))
    .AddPolicy("Especialidades.Alta", p => p.Requirements.Add(new PermisoRequirement("Especialidades", "Alta")))
    .AddPolicy("Especialidades.Baja", p => p.Requirements.Add(new PermisoRequirement("Especialidades", "Baja")))
    .AddPolicy("Especialidades.Modificar", p => p.Requirements.Add(new PermisoRequirement("Especialidades", "Modificar")))
    .AddPolicy("Especialidades.Consultar", p => p.Requirements.Add(new PermisoRequirement("Especialidades", "Consultar")))
    .AddPolicy("Comisiones.Alta", p => p.Requirements.Add(new PermisoRequirement("Comisiones", "Alta")))
    .AddPolicy("Comisiones.Baja", p => p.Requirements.Add(new PermisoRequirement("Comisiones", "Baja")))
    .AddPolicy("Comisiones.Modificar", p => p.Requirements.Add(new PermisoRequirement("Comisiones", "Modificar")))
    .AddPolicy("Comisiones.Consultar", p => p.Requirements.Add(new PermisoRequirement("Comisiones", "Consultar")))
    .AddPolicy("Cursos.Alta", p => p.Requirements.Add(new PermisoRequirement("Cursos", "Alta")))
    .AddPolicy("Cursos.Baja", p => p.Requirements.Add(new PermisoRequirement("Cursos", "Baja")))
    .AddPolicy("Cursos.Modificar", p => p.Requirements.Add(new PermisoRequirement("Cursos", "Modificar")))
    .AddPolicy("Cursos.Consultar", p => p.Requirements.Add(new PermisoRequirement("Cursos", "Consultar")))
    .AddPolicy("Inscripciones.Alta", p => p.Requirements.Add(new PermisoRequirement("Inscripciones", "Alta")))
    .AddPolicy("Inscripciones.Baja", p => p.Requirements.Add(new PermisoRequirement("Inscripciones", "Baja")))
    .AddPolicy("Inscripciones.Modificar", p => p.Requirements.Add(new PermisoRequirement("Inscripciones", "Modificar")))
    .AddPolicy("Inscripciones.Consultar", p => p.Requirements.Add(new PermisoRequirement("Inscripciones", "Consultar")))
    .AddPolicy("DocentesCursos.Alta", p => p.Requirements.Add(new PermisoRequirement("DocentesCursos", "Alta")))
    .AddPolicy("DocentesCursos.Baja", p => p.Requirements.Add(new PermisoRequirement("DocentesCursos", "Baja")))
    .AddPolicy("DocentesCursos.Modificar", p => p.Requirements.Add(new PermisoRequirement("DocentesCursos", "Modificar")))
    .AddPolicy("DocentesCursos.Consultar", p => p.Requirements.Add(new PermisoRequirement("DocentesCursos", "Consultar")));

var app = builder.Build();

// Makes sure the DataBase exists
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TPIContext>();
    context.Database.EnsureCreated();
    DbSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapPersonaEndpoints();
app.MapMateriaEndpoints();
app.MapPlanEndpoints();
app.MapEspecialidadEndpoints();
app.MapComisionEndpoints();
app.MapCursoEndpoints();
app.MapAlumnoInscripcionEndpoints();
app.MapDocenteCursoEndpoints();
app.MapUsuarioEndpoints();
app.MapPerfilEndpoints();

app.Run();
