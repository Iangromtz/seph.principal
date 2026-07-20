using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPeriodos.DTOs;

namespace Seph.Principal.Application.Features.CatPeriodos.Commands.UpdateCatPeriodo
{
    /// <summary>
    /// Comando encargado de actualizar un periodo existente.
    /// </summary>
    public sealed record UpdateCatPeriodoCommand(
        long Id,
        int IntAnio,
        int IntNumeroPeriodo,
        DateTime DateFechaInicio,
        DateTime DateFechaFin)
        : IRequest<ResponseWrapper<CatPeriodoDto>>;

}
