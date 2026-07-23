using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatSectorVinculado.DTOs
{
    public sealed record CatSectorVinculadoDto(
    long Id,
    string StrValor,
    string StrDescripcion
);
}
