using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Infraestructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Infraestructure.Identity
{
    public sealed class GoogleTokenValidator(IOptions<GoogleOptions> options) : IGoogleTokenValidator
    {
        public async Task<GoogleUserPayload?> ValidateAsync(string idToken, CancellationToken cancellationToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = [options.Value.ClientId]
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return new GoogleUserPayload(payload.Email, payload.Name, payload.Subject);
            }
            catch
            {
                return null;
            }
        }
    }
}
