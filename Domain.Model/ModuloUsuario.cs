using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    // Representa el permiso de UN usuario sobre UN módulo
    public class ModuloUsuario
    {
        public int Id { get; private set; }
        public int IdModulo { get; private set; }
        public int IdUsuario { get; private set; }
        public bool PuedeAlta { get; private set; }
        public bool PuedeBaja { get; private set; }
        public bool PuedeModificar { get; private set; }
        public bool PuedeConsultar { get; private set; }

        public ModuloUsuario(int id, int idModulo, int idUsuario,
            bool puedeAlta, bool puedeBaja, bool puedeModificar, bool puedeConsultar)
        {
            SetId(id);
            SetIdModulo(idModulo);
            SetIdUsuario(idUsuario);
            PuedeAlta = puedeAlta;
            PuedeBaja = puedeBaja;
            PuedeModificar = puedeModificar;
            PuedeConsultar = puedeConsultar;
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual que 0.", nameof(id));
            Id = id;
        }

        public void SetIdModulo(int idModulo)
        {
            if (idModulo <= 0)
                throw new ArgumentException("El IdModulo debe ser mayor que 0.", nameof(idModulo));
            IdModulo = idModulo;
        }

        public void SetIdUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("El IdUsuario debe ser mayor que 0.", nameof(idUsuario));
            IdUsuario = idUsuario;
        }
    }
}
