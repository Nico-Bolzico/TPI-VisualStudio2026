namespace Domain.Model
{
    public class DocenteCurso
    {
        public int Id { get; private set; }
        public int IdCurso { get; private set; }
        public int IdDocente { get; private set; }
        public int Cargo { get; private set; }

        public DocenteCurso(int id, int idCurso, int idDocente, int cargo)
        {
            SetId(id);
            SetIdCurso(idCurso);
            SetIdDocente(idDocente);
            SetCargo(cargo);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual que 0.", nameof(id));
            Id = id;
        }

        public void SetIdCurso(int idCurso)
        {
            if (idCurso <= 0)
                throw new ArgumentException("El IdCurso debe ser mayor que 0.", nameof(idCurso));
            IdCurso = idCurso;
        }

        public void SetIdDocente(int idDocente)
        {
            if (idDocente <= 0)
                throw new ArgumentException("El IdDocente debe ser mayor que 0.", nameof(idDocente));
            IdDocente = idDocente;
        }

        public void SetCargo(int cargo)
        {
            if (cargo <= 0)
                throw new ArgumentException("El cargo debe ser mayor que 0.", nameof(cargo));
            Cargo = cargo;
        }
    }
}
