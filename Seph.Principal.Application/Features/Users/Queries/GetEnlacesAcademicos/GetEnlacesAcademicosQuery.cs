using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Users.DTOs;

namespace Seph.Principal.Application.Features.Users.Queries.GetEnlacesAcademicos
{
    public sealed record GetEnlacesAcademicosQuery : IRequest<ResponseWrapper<List<EnlaceAcademicoDto>>>;
}
