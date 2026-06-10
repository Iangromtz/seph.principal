using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Infraestructure.Authentication
{
    public sealed class GoogleOptions
    {
        public const string SectionName = "Google";
        public string ClientId { get; init; } = string.Empty;
    }
}
