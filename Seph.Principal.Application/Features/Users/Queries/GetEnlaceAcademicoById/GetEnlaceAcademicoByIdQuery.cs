using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Users.DTOs;

namespace Seph.Principal.Application.Features.Users.Queries.GetEnlaceAcademicoById
{
    public sealed record GetEnlaceAcademicoByIdQuery(Guid Id) : IRequest<ResponseWrapper<EnlaceAcademicoDto>>;
}
