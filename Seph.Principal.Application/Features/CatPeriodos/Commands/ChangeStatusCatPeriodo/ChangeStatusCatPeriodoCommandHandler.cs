using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPeriodos.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatPeriodos.Commands.ChangeStatusCatPeriodo
{
    /// <summary>
    /// Activa o desactiva un periodo existente.
    /// </summary>
    public sealed class ChangeStatusCatPeriodoCommandHandler(
        ICatPeriodoRepository catPeriodoRepository,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
        : IRequestHandler<
            ChangeStatusCatPeriodoCommand,
            ResponseWrapper<CatPeriodoDto>>
    {
        public async Task<ResponseWrapper<CatPeriodoDto>> Handle(
            ChangeStatusCatPeriodoCommand request,
            CancellationToken cancellationToken)
        {
            // Busca el periodo solicitado.
            var catPeriodo = await catPeriodoRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (catPeriodo is null)
            {
                return ResponseFactory.Failure<CatPeriodoDto>(
                    "No se encontró el periodo solicitado.",
                    HttpStatusCode.NotFound);
            }

            // Guarda el estado anterior para la bitácora.
            var estadoAnterior = catPeriodo.BitActivo;

            // Aplica el nuevo estado indicado por el frontend.
            catPeriodo.BitActivo = request.BitActivo;

            catPeriodoRepository.Update(catPeriodo);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await bitacoraService.RegistrarAsync(
                "CatPeriodo",
                catPeriodo.Id.ToString(),
                request.BitActivo ? "Activar" : "Desactivar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                new
                {
                    BitActivoAnterior = estadoAnterior,
                    BitActivoActual = catPeriodo.BitActivo
                },
                cancellationToken);

            var dto = new CatPeriodoDto(
                catPeriodo.Id,
                catPeriodo.StrValor,
                catPeriodo.StrDescripcion,
                catPeriodo.IntAnio,
                catPeriodo.IntNumeroPeriodo,
                catPeriodo.DateFechaInicio,
                catPeriodo.DateFechaFin,
                catPeriodo.BitActivo,
                catPeriodo.IdTipoPeriodo,
                catPeriodo.TipoPeriodo.StrValor);

            var message = request.BitActivo
                ? "Periodo activado correctamente"
                : "Periodo desactivado correctamente";

            return ResponseFactory.Success(
                dto,
                message);
        }
    }
}
