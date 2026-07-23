using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatTipoPeriodo.DTOs;

namespace Seph.Principal.Application.Features.CatTipoPeriodo.Queries
{
    /// <summary>
    /// Consulta los tipos de periodo registrados.
    /// </summary>
    public sealed record GetCatTipoPeriodoQuery()
        : IRequest<
            ResponseWrapper<
                IReadOnlyList<CatTipoPeriodoDto>>>;
}
