using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class CatTipoPeriodo
    {
        public long Id { get; set; }

        public string StrValor { get; set; } = string.Empty;

        public string StrDescripcion { get; set; } = string.Empty;

        public int IntNumeroMeses { get; set; }

        public bool BitActivo { get; set; }

        #region Constructor

        public CatTipoPeriodo()
        {

        }

        public CatTipoPeriodo(
            long id,
            string strValor,
            string strDescripcion,
            int intNumeroMeses,
            bool bitActivo)
        {
            Id = id;
            StrValor = strValor;
            StrDescripcion = strDescripcion;
            IntNumeroMeses = intNumeroMeses;
            BitActivo = bitActivo;
        }

        public CatTipoPeriodo(string strValor)
        {
            StrValor = strValor;
        }

        #endregion
    }
}
