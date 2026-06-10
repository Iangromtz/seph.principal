using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Common.Interfaces
{
    public sealed record GoogleUserPayload(string Email, string Name, string Subject);

    public interface IGoogleTokenValidator
    {
        Task<GoogleUserPayload?> ValidateAsync(string idToken, CancellationToken cancellationToken);
    }
}