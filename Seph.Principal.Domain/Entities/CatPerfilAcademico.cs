using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class CatPerfilAcademico
    {
        public long Id { get; set; }

        public string StrValor { get; set; } = string.Empty;

        public string StrDescripcion { get; set; } = string.Empty;

        #region Constructor
        public CatPerfilAcademico()
        {

        }

        public CatPerfilAcademico(string strValor)
        {
            StrValor = strValor;
        }
        #endregion
    }
}
