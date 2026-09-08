using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Modulo
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }
        public bool Activo { get; private set; }

        public Modulo(int id, string descripcion, bool activo)
        {
            SetId(id);
            SetDescripcion(descripcion);
            Activo = activo;
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

        public void SetActivo(bool activo) => Activo = activo;
    }
}
