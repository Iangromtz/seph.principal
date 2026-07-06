using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPerfilAcademico.DTOs;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.CatPerfilAcademico.Queries.GetCatPerfilAcademico
{
    public sealed record GetCatPerfilAcademicoQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<CatPerfilAcademicoDto>>>;
}
