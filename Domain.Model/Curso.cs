namespace Domain.Model
{
    public class Curso
    {
        public int Id { get; private set; }
        public int IdMateria { get; private set; }
        public int IdComision { get; private set; }
        public int AnioCalendario { get; private set; }
        public int Cupo { get; private set; }

        public Curso(int id, int idMateria, int idComision, int anioCalendario, int cupo)
        {
            SetId(id);
            SetIdMateria(idMateria);
            SetIdComision(idComision);
            SetAnioCalendario(anioCalendario);
            SetCupo(cupo);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual que 0.", nameof(id));
            Id = id;
        }

        public void SetIdMateria(int idMateria)
        {
            if (idMateria <= 0)
                throw new ArgumentException("El IdMateria debe ser mayor que 0.", nameof(idMateria));
            IdMateria = idMateria;
        }

        public void SetIdComision(int idComision)
        {
            if (idComision <= 0)
                throw new ArgumentException("El IdComision debe ser mayor que 0.", nameof(idComision));
            IdComision = idComision;
        }

        public void SetAnioCalendario(int anioCalendario)
        {
            if (anioCalendario < 1900 || anioCalendario > 2100)
                throw new ArgumentException("El año calendario debe ser un año válido.", nameof(anioCalendario));
            AnioCalendario = anioCalendario;
        }

        public void SetCupo(int cupo)
        {
            if (cupo < 0)
                throw new ArgumentException("El cupo no puede ser negativo.", nameof(cupo));
            Cupo = cupo;
        }
    }
}
