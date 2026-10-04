namespace Domain.Model
{
    public class Plan
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }
        public int IdEspecialidad { get; private set; }
        public List<Materia> Materias { get; private set; } = new();

        public Plan(int id, string descripcion, int idEspecialidad)
        {
            SetId(id);
            SetDescripcion(descripcion);
            SetIdEspecialidad(idEspecialidad);
        }

        public Plan(int id, string descripcion, int idEspecialidad, IEnumerable<Materia>? materias)
            : this(id, descripcion, idEspecialidad)
        {
            if (materias != null)
            {
                Materias.AddRange(materias);
            }
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

        public void SetIdEspecialidad(int idEspecialidad)
        {
            if (idEspecialidad <= 0)
                throw new ArgumentException("El IdEspecialidad debe ser mayor que 0.", nameof(idEspecialidad));
            IdEspecialidad = idEspecialidad;
        }

        public void AgregarMateria(Materia materia)
        {
            ArgumentNullException.ThrowIfNull(materia);
            Materias.Add(materia);
        }

        public void QuitarMateria(int idMateria)
        {
            var materia = Materias.FirstOrDefault(m => m.Id == idMateria);
            if (materia != null)
            {
                Materias.Remove(materia);
            }
        }

        public void LimpiarMaterias()
        {
            Materias.Clear();
        }
    }
}
