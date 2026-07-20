using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatDiscapacitado.DTOs
{
    public sealed record CatDiscapacitadoDto(
    long Id,
    string StrValor,
    string StrDescripcion
);
}
