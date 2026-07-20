using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPeriodos.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatPeriodos.Commands.CreateCatPeriodo
{
    /// <summary>
    /// Registra un nuevo periodo en el catálogo.
    /// El nombre visible se genera automáticamente con base
    /// en las fechas de inicio y fin.
    /// </summary>
    public sealed class CreateCatPeriodoCommandHandler(
        ICatPeriodoRepository catPeriodoRepository,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CreateCatPeriodoCommand, ResponseWrapper<CatPeriodoDto>>
    {
        public async Task<ResponseWrapper<CatPeriodoDto>> Handle(
            CreateCatPeriodoCommand request,
            CancellationToken cancellationToken)
        {
            // Evita registrar dos veces el mismo número de periodo
            // dentro del mismo año.
            var exists = await catPeriodoRepository.ExistsByAnioNumeroPeriodoAsync(
                request.IntAnio,
                request.IntNumeroPeriodo,
                cancellationToken);

            if (exists)
            {
                return ResponseFactory.Failure<CatPeriodoDto>(
                    "Ya existe un periodo registrado con el mismo año y número de periodo.",
                    HttpStatusCode.BadRequest);
            }

            // Genera el identificador corto del periodo.
            var strValor = $"{request.IntAnio}-{request.IntNumeroPeriodo}";

            // Genera la descripción visible a partir de las fechas.
            var strDescripcion = GeneratePeriodName(
                request.DateFechaInicio,
                request.DateFechaFin);

            var catPeriodo = new CatPeriodo
            {
                StrDescripcion = strDescripcion,
                IntAnio = request.IntAnio,
                IntNumeroPeriodo = request.IntNumeroPeriodo,
                DateFechaInicio = request.DateFechaInicio,
                DateFechaFin = request.DateFechaFin,
                BitActivo = true
            };

            await catPeriodoRepository.AddAsync(
                catPeriodo,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await bitacoraService.RegistrarAsync(
                "CatPeriodo",
                catPeriodo.Id.ToString(),
                "Agregar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                catPeriodo,
                cancellationToken);

            var dto = new CatPeriodoDto(
                catPeriodo.Id,
                catPeriodo.StrValor,
                catPeriodo.StrDescripcion,
                catPeriodo.IntAnio,
                catPeriodo.IntNumeroPeriodo,
                catPeriodo.DateFechaInicio,
                catPeriodo.DateFechaFin,
                catPeriodo.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Periodo registrado correctamente");
        }

        /// <summary>
        /// Genera el nombre del periodo utilizando los nombres
        /// de los meses en español.
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

            mesInicio = char.ToUpper(mesInicio[0]) + mesInicio[1..];
            mesFin = char.ToUpper(mesFin[0]) + mesFin[1..];

            if (fechaInicio.Year == fechaFin.Year)
            {
                return $"{mesInicio} - {mesFin} {fechaInicio.Year}";
            }

            return $"{mesInicio} {fechaInicio.Year} - {mesFin} {fechaFin.Year}";
        }
    }
}
