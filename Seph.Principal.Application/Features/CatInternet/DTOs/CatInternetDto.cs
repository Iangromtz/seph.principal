using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatInternet.DTOs
{
    public sealed record CatInternetDto(
    long Id,
    string StrValor,
    string StrDescripcion
);
}
