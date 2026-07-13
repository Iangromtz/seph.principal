using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPeriodos.DTOs;

namespace Seph.Principal.Application.Features.CatPeriodos.Queries.GetCatPeriodo
{
    public sealed record GetCatPeriodoQuery()
       : IRequest<ResponseWrapper<IReadOnlyList<CatPeriodoDto>>>;
}
