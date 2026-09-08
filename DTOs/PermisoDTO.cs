using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class PermisoDTO
    {
        public string Modulo { get; set; } = string.Empty;
        public bool PuedeAlta { get; set; }
        public bool PuedeBaja { get; set; }
        public bool PuedeModificar { get; set; }
        public bool PuedeConsultar { get; set; }
    }
}
