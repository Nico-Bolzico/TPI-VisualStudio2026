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
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    if (!context.Usuarios.Any())
    {
        var personaAdmin = new Domain.Model.Persona(
            id: 0,
            legajo: 1,
            nombre: "Administrador",
            apellido: "Sistema",
            direccion: "Sin especificar",
            email: "admin@academia.edu",
            telefono: "Sin especificar",
            fechaNacimiento: new DateTime(1990, 1, 1),
            tipoPersona: Domain.Model.TipoPersona.Profesor,
            idPlan: null);

        context.Personas.Add(personaAdmin);
        context.SaveChanges();

        var passwordHash = Application.Services.PasswordHasher.Hash("admin123");
        context.Usuarios.Add(new Domain.Model.Usuario(
            id: 0,
            nombreUsuario: "admin",
            passwordHash: passwordHash,
            habilitado: true,
            cambiaClave: false,
            idPersona: personaAdmin.Id));
        context.SaveChanges();
    }

    if (!context.Modulos.Any())
    {
        var moduloPersonas = new Domain.Model.Modulo(0, "Personas", true);
        var moduloMaterias = new Domain.Model.Modulo(0, "Materias", true);
        var moduloPlanes = new Domain.Model.Modulo(0, "Planes", true);
        var moduloEspecialidades = new Domain.Model.Modulo(0, "Especialidades", true);
        var moduloComisiones = new Domain.Model.Modulo(0, "Comisiones", true);
        var moduloCursos = new Domain.Model.Modulo(0, "Cursos", true);
        var moduloInscripciones = new Domain.Model.Modulo(0, "Inscripciones", true);
        var moduloDocentesCursos = new Domain.Model.Modulo(0, "DocentesCursos", true);
        context.Modulos.AddRange(moduloPersonas, moduloMaterias, moduloPlanes, moduloEspecialidades, moduloComisiones, moduloCursos, moduloInscripciones, moduloDocentesCursos);
        context.SaveChanges();

        var admin = context.Usuarios.First(u => u.NombreUsuario == "admin");

        context.ModulosUsuarios.AddRange(
            new Domain.Model.ModuloUsuario(0, moduloPersonas.Id, admin.Id, true, true, true, true),
            new Domain.Model.ModuloUsuario(0, moduloMaterias.Id, admin.Id, true, true, true, true),
            new Domain.Model.ModuloUsuario(0, moduloPlanes.Id, admin.Id, true, true, true, true),
            new Domain.Model.ModuloUsuario(0, moduloEspecialidades.Id, admin.Id, true, true, true, true),
            new Domain.Model.ModuloUsuario(0, moduloComisiones.Id, admin.Id, true, true, true, true),
            new Domain.Model.ModuloUsuario(0, moduloCursos.Id, admin.Id, true, true, true, true),
            new Domain.Model.ModuloUsuario(0, moduloInscripciones.Id, admin.Id, true, true, true, true),
            new Domain.Model.ModuloUsuario(0, moduloDocentesCursos.Id, admin.Id, true, true, true, true)
        );
        context.SaveChanges();
    }

    if (!context.Especialidades.Any())
    {
        context.Especialidades.Add(new Domain.Model.Especialidad(0, "Ingeniería en Sistemas de Información"));
        context.SaveChanges();
    }

    if (!context.Comisiones.Any())
    {
        context.Comisiones.Add(new Domain.Model.Comision(0, "1K1", 1, 1));
        context.SaveChanges();
    }

    if (!context.Cursos.Any())
    {
        context.Cursos.Add(new Domain.Model.Curso(0, 1, 1, 2026, 35));
        context.SaveChanges();
    }
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

app.Run();
