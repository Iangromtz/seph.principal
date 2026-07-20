using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPeriodos.DTOs;

namespace Seph.Principal.Application.Features.CatPeriodos.Commands.ChangeStatusCatPeriodo
{
    /// <summary>
    /// Comando encargado de activar o desactivar un periodo.
    /// </summary>
    public sealed record ChangeStatusCatPeriodoCommand(
        long Id,
        bool BitActivo)
        : IRequest<ResponseWrapper<CatPeriodoDto>>;
}
