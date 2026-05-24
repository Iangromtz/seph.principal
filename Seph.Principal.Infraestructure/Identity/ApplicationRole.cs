using Microsoft.AspNetCore.Identity;

namespace Seph.Principal.Infraestructure.Identity
{
    public sealed  class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;
    }
}
