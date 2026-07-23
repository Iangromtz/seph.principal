using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatMecanismoSeguimiento.DTOs;
using Seph.Principal.Application.Features.CatSectorVinculado.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatSectorVinculado.Queries.GetSectorVinculado
{
    public sealed record GetCatSectorVinculadoQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<CatSectorVinculadoDto>>>;
}

