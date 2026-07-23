using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class CatPeriodo
    {
        public long Id { get; set; }

        public string StrValor { get; set; } = string.Empty;

        public string StrDescripcion { get; set; } = string.Empty;

        public int IntAnio { get; set; }

        public int IntNumeroPeriodo { get; set; }

        public DateTime DateFechaInicio { get; set; }

        public DateTime DateFechaFin { get; set; }

        public bool BitActivo { get; set; }

        public long IdTipoPeriodo { get; set; }

        public CatTipoPeriodo TipoPeriodo { get; set; } = null!;

        #region Constructor

        public CatPeriodo()
        {

        }

        public CatPeriodo(
            long id,
            string strValor,
            string strDescripcion,
            int intAnio,
            int intNumeroPeriodo,
            DateTime dateFechaInicio,
            DateTime dateFechaFin,
            bool bitActivo,
            long idTipoPeriodo)
        {
            Id = id;
            StrValor = strValor;
            StrDescripcion = strDescripcion;
            IntAnio = intAnio;
            IntNumeroPeriodo = intNumeroPeriodo;
            DateFechaInicio = dateFechaInicio;
            DateFechaFin = dateFechaFin;
            BitActivo = bitActivo;
            IdTipoPeriodo = idTipoPeriodo;
        }

        public CatPeriodo(string strValor)
        {
            StrValor = strValor;
        }

        #endregion
    }
}
