using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public record PermisoUsuarioInfo(string Modulo, bool PuedeAlta, bool PuedeBaja, bool PuedeModificar, bool PuedeConsultar);
}
