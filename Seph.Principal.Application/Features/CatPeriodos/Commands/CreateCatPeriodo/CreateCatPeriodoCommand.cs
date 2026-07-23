using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPeriodos.DTOs;

namespace Seph.Principal.Application.Features.CatPeriodos.Commands.CreateCatPeriodo
{
    /// <summary>
    /// Comando para registrar un nuevo periodo.
    /// El nombre visible del periodo se genera automáticamente
    /// utilizando las fechas de inicio y fin.
    /// </summary>
    public sealed record CreateCatPeriodoCommand(
        int IntAnio,
        int IntNumeroPeriodo,
        DateTime DateFechaInicio,
        long IdTipoPeriodo,
        DateTime DateFechaFin)
        : IRequest<ResponseWrapper<CatPeriodoDto>>;
}
