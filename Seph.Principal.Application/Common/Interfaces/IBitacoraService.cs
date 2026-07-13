using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Common.Interfaces
{
    public interface IBitacoraService
    {
        Task RegistrarAsync(string modulo, string idRegistro, string accion, string idUsuario, string usuarioNombre, object entidad, CancellationToken cancellationToken);
    }
}
