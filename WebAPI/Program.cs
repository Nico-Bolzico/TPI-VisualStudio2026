using Application.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework Context
builder.Services.AddDbContext<TPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TPIConnection")));

// Add Dependency Injection
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
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
    .AddPolicy("Materias.Consultar", p => p.Requirements.Add(new PermisoRequirement("Materias", "Consultar")));

var app = builder.Build();

// Makes sure the DataBase exists
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TPIContext>();
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
        context.Modulos.AddRange(moduloPersonas, moduloMaterias);
        context.SaveChanges();

        var admin = context.Usuarios.First(u => u.NombreUsuario == "admin");

        context.ModulosUsuarios.AddRange(
            new Domain.Model.ModuloUsuario(0, moduloPersonas.Id, admin.Id, true, true, true, true),
            new Domain.Model.ModuloUsuario(0, moduloMaterias.Id, admin.Id, true, true, true, true)
        );
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
app.MapUsuarioEndpoints();

app.Run();

