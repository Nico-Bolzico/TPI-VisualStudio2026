namespace Domain.Model
{
    public class Comision
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }
        public int AnioEspecialidad { get; private set; }
        public int IdPlan { get; private set; }

        public Comision(int id, string descripcion, int anioEspecialidad, int idPlan)
        {
            SetId(id);
            SetDescripcion(descripcion);
            SetAnioEspecialidad(anioEspecialidad);
            SetIdPlan(idPlan);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual que 0.", nameof(id));
            Id = id;
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción no puede ser nula o vacía.", nameof(descripcion));
            Descripcion = descripcion;
        }

        public void SetAnioEspecialidad(int anioEspecialidad)
        {
            if (anioEspecialidad <= 0)
                throw new ArgumentException("El año de especialidad debe ser mayor que 0.", nameof(anioEspecialidad));
            AnioEspecialidad = anioEspecialidad;
        }

        public void SetIdPlan(int idPlan)
        {
            if (idPlan <= 0)
                throw new ArgumentException("El IdPlan debe ser mayor que 0.", nameof(idPlan));
            IdPlan = idPlan;
        }
    }
}
