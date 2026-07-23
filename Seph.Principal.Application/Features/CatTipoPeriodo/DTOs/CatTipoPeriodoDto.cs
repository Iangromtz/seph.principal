using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoPeriodo.DTOs
{
    public sealed record CatTipoPeriodoDto(
          long Id,
          string StrValor,
          string StrDescripcion,
          int IntNumeroMeses,
          bool BitActivo);
}
