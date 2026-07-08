using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatNivelAcademico.DTOs;

namespace Seph.Principal.Application.Features.CatNivelAcademico.Queries.GetCatNivelAcademico
{
    public sealed record GetCatNivelAcademicoQuery : IRequest<ResponseWrapper<List<CatNivelAcademicoDto>>>;
}
