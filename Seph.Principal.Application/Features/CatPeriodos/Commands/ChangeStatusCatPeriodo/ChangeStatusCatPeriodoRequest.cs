using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatPeriodos.Commands.ChangeStatusCatPeriodo
{
    /// <summary>
    /// Datos necesarios para cambiar el estado
    /// de un periodo.
    /// </summary>
    public sealed record ChangeStatusCatPeriodoRequest(
        bool BitActivo);
}
