using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatDiscapacitado.DTOs;
using Seph.Principal.Application.Features.CatArea.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatDiscapacitado.Queries.GetDiscapacitado
{
    public sealed record GetCatDiscapacitadoQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<CatDiscapacitadoDto>>>;
}

