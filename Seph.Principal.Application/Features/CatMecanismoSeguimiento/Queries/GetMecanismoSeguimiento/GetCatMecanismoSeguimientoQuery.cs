using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatMecanismoSeguimiento.DTOs;
using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatMecanismoSeguimiento.Queries.GetMecanismoSeguimiento
{
    public sealed record GetCatMecanismoSeguimientoQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<CatMecanismoSeguimientoDto>>>;
}

