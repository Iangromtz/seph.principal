using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Infraestructure.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Seph.Principal.Infraestructure.Bitacora
{
    public sealed class BitacoraService(HttpClient httpClient, IOptions<JwtOptions> jwtOptions) : IBitacoraService
    {
        public async Task RegistrarAsync(string modulo, string idRegistro, string accion, string idUsuario, string usuarioNombre, object entidad, CancellationToken cancellationToken)
        {
            var payload = new
            {
                modulo,
                idRegistro,
                accion,
                idUsuario,
                usuarioNombre,
                jsonData = JsonSerializer.Serialize(entidad)
            };

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/bitacora")
                {
                    Content = JsonContent.Create(payload)
                };
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GenerarTokenInterno());

                await httpClient.SendAsync(request, cancellationToken);
            }
            catch { }
        }


        private string GenerarTokenInterno()
        {
            var options = jwtOptions.Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: [new Claim(ClaimTypes.Name, "seph-principal-system")],
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}