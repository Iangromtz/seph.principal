using System.Globalization;
using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPeriodos.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatPeriodos.Commands.UpdateCatPeriodo
{
    /// <summary>
    /// Actualiza los datos de un periodo existente.
    /// </summary>
    public sealed class UpdateCatPeriodoCommandHandler(
        ICatPeriodoRepository catPeriodoRepository,
        ICatTipoPeriodoRepository catTipoPeriodoRepository,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateCatPeriodoCommand, ResponseWrapper<CatPeriodoDto>>
    {
        public async Task<ResponseWrapper<CatPeriodoDto>> Handle(
            UpdateCatPeriodoCommand request,
            CancellationToken cancellationToken)
        {
            // Busca el periodo que se desea actualizar.
            var catPeriodo = await catPeriodoRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (catPeriodo is null)
            {
                return ResponseFactory.Failure<CatPeriodoDto>(
                    "No se encontró el periodo solicitado.",
                    HttpStatusCode.NotFound);
            }

            // Valida que exista el tipo de periodo seleccionado.
            var tipoPeriodo = await catTipoPeriodoRepository.GetByIdAsync(
                request.IdTipoPeriodo,
                cancellationToken);

            if (tipoPeriodo is null)
            {
                return ResponseFactory.Failure<CatPeriodoDto>(
                    "El tipo de periodo seleccionado no existe.",
                    HttpStatusCode.BadRequest);
            }

            if (!tipoPeriodo.BitActivo)
            {
                return ResponseFactory.Failure<CatPeriodoDto>(
                    "El tipo de periodo seleccionado se encuentra inactivo.",
                    HttpStatusCode.BadRequest);
            }

            // Verifica que no exista otro periodo con el mismo año y número.
            var exists = await catPeriodoRepository
                .ExistsByAnioNumeroPeriodoExceptIdAsync(
                    request.IntAnio,
                    request.IntNumeroPeriodo,
                    request.Id,
                    cancellationToken);

            if (exists)
            {
                return ResponseFactory.Failure<CatPeriodoDto>(
                    "Ya existe otro periodo registrado con el mismo año y número de periodo.",
                    HttpStatusCode.BadRequest);
            }

            // Guarda una copia de los datos anteriores para la bitácora.
            var datosAnteriores = new
            {
                catPeriodo.Id,
                catPeriodo.StrValor,
                catPeriodo.StrDescripcion,
                catPeriodo.IntAnio,
                catPeriodo.IntNumeroPeriodo,
                catPeriodo.DateFechaInicio,
                catPeriodo.DateFechaFin,
                catPeriodo.BitActivo,
                catPeriodo.IdTipoPeriodo
            };

            // Genera nuevamente los valores automáticos.
            var strValor =
                $"{request.IntAnio}-{request.IntNumeroPeriodo}";

            var strDescripcion = GeneratePeriodName(
                request.DateFechaInicio,
                request.DateFechaFin);

            // Actualiza únicamente los datos editables.
            catPeriodo.StrValor = strValor;
            catPeriodo.StrDescripcion = strDescripcion;
            catPeriodo.IntAnio = request.IntAnio;
            catPeriodo.IntNumeroPeriodo = request.IntNumeroPeriodo;
            catPeriodo.DateFechaInicio = request.DateFechaInicio;
            catPeriodo.DateFechaFin = request.DateFechaFin;
            catPeriodo.IdTipoPeriodo = request.IdTipoPeriodo;

            catPeriodoRepository.Update(catPeriodo);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            await bitacoraService.RegistrarAsync(
                "CatPeriodo",
                catPeriodo.Id.ToString(),
                "Actualizar",
                currentUserService.UserId?.ToString()
                    ?? "desconocido",
                currentUserService.Email?.ToString()
                    ?? "desconocido",
                new
                {
                    Anterior = datosAnteriores,
                    Actual = catPeriodo
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
                tipoPeriodo.StrValor);

            return ResponseFactory.Success(
                dto,
                "Periodo actualizado correctamente");
        }

        /// <summary>
        /// Genera la descripción del periodo utilizando
        /// los nombres de los meses en español.
        /// </summary>
        private static string GeneratePeriodName(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            var culture = new CultureInfo("es-MX");

            var mesInicio = culture.DateTimeFormat
                .GetMonthName(fechaInicio.Month);

            var mesFin = culture.DateTimeFormat
                .GetMonthName(fechaFin.Month);

            mesInicio =
                char.ToUpper(mesInicio[0]) + mesInicio[1..];

            mesFin =
                char.ToUpper(mesFin[0]) + mesFin[1..];

            if (fechaInicio.Year == fechaFin.Year)
            {
                return
                    $"{mesInicio} - {mesFin} {fechaInicio.Year}";
            }

            return
                $"{mesInicio} {fechaInicio.Year} - " +
                $"{mesFin} {fechaFin.Year}";
        }
    }
}