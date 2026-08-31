using Application.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using WebAPI;

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

// Map endpoints
app.MapPersonaEndpoints();
app.MapMateriaEndpoints();

app.Run();
