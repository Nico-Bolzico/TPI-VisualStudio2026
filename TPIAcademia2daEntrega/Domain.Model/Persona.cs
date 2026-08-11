using System.Text.RegularExpressions;

namespace Domain.Model
{
    public class Persona
    {
        public int Id { get; private set; }
        public int Legajo { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string Direccion { get; private set; }
        public string Email { get; private set; }
        public string Telefono { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public TipoPersona TipoPersona { get; private set; }
        public int? IdPlan { get; private set; }

        public Persona(int id, int legajo, string nombre, string apellido, string direccion,
            string email, string telefono, DateTime fechaNacimiento, TipoPersona tipoPersona, int? idPlan)
        {
            SetId(id);
            SetLegajo(legajo);
            SetNombre(nombre);
            SetApellido(apellido);
            SetDireccion(direccion);
            SetEmail(email);
            SetTelefono(telefono);
            SetFechaNacimiento(fechaNacimiento);
            SetTipoPersona(tipoPersona);
            SetIdPlan(idPlan);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual que 0.", nameof(id));
            Id = id;
        }

        public void SetLegajo(int legajo)
        {
            if (legajo <= 0)
                throw new ArgumentException("El Legajo debe ser mayor que 0.", nameof(legajo));
            Legajo = legajo;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede ser nulo o vacío.", nameof(apellido));
            Apellido = apellido;
        }

        public void SetDireccion(string direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion))
                throw new ArgumentException("La dirección no puede ser nula o vacía.", nameof(direccion));
            Direccion = direccion;
        }

        public void SetEmail(string email)
        {
            if (!EsEmailValido(email))
                throw new ArgumentException("El email no tiene un formato válido.", nameof(email));
            Email = email;
        }

        private static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public void SetTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono no puede ser nulo o vacío.", nameof(telefono));
            Telefono = telefono;
        }

        public void SetFechaNacimiento(DateTime fechaNacimiento)
        {
            if (fechaNacimiento == default)
                throw new ArgumentException("La fecha de nacimiento no puede ser nula.", nameof(fechaNacimiento));
            if (fechaNacimiento > DateTime.Now)
                throw new ArgumentException("La fecha de nacimiento no puede ser futura.", nameof(fechaNacimiento));
            FechaNacimiento = fechaNacimiento;
        }

        public void SetTipoPersona(TipoPersona tipoPersona)
        {
            TipoPersona = tipoPersona;
        }

        public void SetIdPlan(int? idPlan)
        {
            if (TipoPersona == TipoPersona.Alumno)
            {
                if (idPlan is null || idPlan <= 0)
                    throw new ArgumentException("Un Alumno debe tener un Plan asignado.", nameof(idPlan));
            }
            else if (idPlan is not null && idPlan < 0)
            {
                throw new ArgumentException("El IdPlan no puede ser negativo.", nameof(idPlan));
            }
            IdPlan = idPlan;
        }
    }
}
