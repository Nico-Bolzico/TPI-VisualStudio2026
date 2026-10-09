namespace Domain.Model
{
    public class Especialidad
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }

        public Especialidad(int id, string descripcion)
        {
            SetId(id);
            SetDescripcion(descripcion);
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
    }
}
