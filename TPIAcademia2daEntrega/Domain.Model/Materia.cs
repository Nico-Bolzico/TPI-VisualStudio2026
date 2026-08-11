namespace Domain.Model
{
    public class Materia
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }
        public int HsSemanales { get; private set; }
        public int HsTotales { get; private set; }
        public int IdPlan { get; private set; }

        public Materia(int id, string descripcion, int hsSemanales, int hsTotales, int idPlan)
        {
            SetId(id);
            SetDescripcion(descripcion);
            SetIdPlan(idPlan);
            SetHoras(hsSemanales, hsTotales);
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

        public void SetIdPlan(int idPlan)
        {
            if (idPlan <= 0)
                throw new ArgumentException("El IdPlan debe ser mayor que 0.", nameof(idPlan));
            IdPlan = idPlan;
        }

        public void SetHoras(int hsSemanales, int hsTotales)
        {
            if (hsSemanales <= 0)
                throw new ArgumentException("Las horas semanales deben ser mayores que 0.", nameof(hsSemanales));
            if (hsTotales <= 0)
                throw new ArgumentException("Las horas totales deben ser mayores que 0.", nameof(hsTotales));
            if (hsTotales < hsSemanales)
                throw new ArgumentException("Las horas totales no pueden ser menores que las horas semanales.", nameof(hsTotales));

            HsSemanales = hsSemanales;
            HsTotales = hsTotales;
        }
    }
}
