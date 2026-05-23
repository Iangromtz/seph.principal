using MediatR;
using Microsoft.EntityFrameworkCore;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Administration.Queries.GetDashboard
{
    public sealed class GetDashboardQueryHandler(IApplicationDbContext dbContext):
        IRequestHandler<GetDashboardQuery,ResponseWrapper<DashboardSummaryDto>>
    {
        public async Task<ResponseWrapper<DashboardSummaryDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var activeSessions =  await dbContext.RefreshTokenSessions.CountAsync(
                session =>session.ExpiresAtUtc > DateTimeOffset.UtcNow,cancellationToken);

            var summary = new DashboardSummaryDto(
             ActiveUsers: 128,
             ActiveSessions: activeSessions,
             SecurityAlerts: 3,
             ApiAvailability: 99.98m,
             RecentActivities:
             [
                 new("Inicio de sesión administrativo", "admin@seph.local", DateTimeOffset.UtcNow.AddMinutes(-8), "info"),
                new("Política MFA actualizada", "security@seph.local", DateTimeOffset.UtcNow.AddMinutes(-32), "success"),
                new("Intento de acceso bloqueado", "gateway", DateTimeOffset.UtcNow.AddHours(-2), "warning")
             ]);
            return ResponseFactory.Success(summary,"Resumen administrativo obtenido correctamente");
        }
    }
}
