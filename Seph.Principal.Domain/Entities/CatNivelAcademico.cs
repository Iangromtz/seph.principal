using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class CatNivelAcademico
    {
        public long Id { get; set; }
        public string StrValor { get; set; } = string.Empty;
        public string StrDescripcion { get; set; } = string.Empty;

        public CatNivelAcademico() { }

        public CatNivelAcademico(string strValor)
        {
            StrValor = strValor;
        }
    }
}
