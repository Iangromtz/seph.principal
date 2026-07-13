using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class ReporteMatricula
    {
        public long Id { get; set; }

        public long IdMapInstitucionPeriodo { get; set; }

        public int IntTotal { get; set; }

        public int IntTotalHombres { get; set; }

        public int IntTotalMujeres { get; set; }

        public int IntTsu { get; set; }

        public int IntLicenciatura { get; set; }

        public int IntPostgrado { get; set; }

        public decimal DecimalTazaDesercion { get; set; }

        public decimal DecimalTazaReprobacion { get; set; }

        public decimal DecimalTazaEficienciaTerminal { get; set; }

        public DateTime DateTimeFechaRegistro { get; set; }

        public Guid IdUsuarioRegistro { get; set; }

        public bool BitActivo { get; set; }

        #region Constructor

        public ReporteMatricula()
        {

        }

        public ReporteMatricula(
            long id,
            long idMapInstitucionPeriodo,
            int intMatriculaTotal,
            int intMatriculaHombres,
            int intMatriculaMujeres,
            int intMatriculaTsu,
            int intMatriculaLicenciatura,
            int intMatriculaPosgrado,
            decimal decTasaDesercion,
            decimal decTasaReprobacion,
            decimal decTasaEficienciaTerminal,
            DateTime dateTimeFechaRegistro,
            Guid idUsuarioRegistro,
            bool bitActivo)
        {
            Id = id;
            IdMapInstitucionPeriodo = idMapInstitucionPeriodo;
            IntTotal = intMatriculaTotal;
            IntTotalHombres = intMatriculaHombres;
            IntTotalMujeres = intMatriculaMujeres;
            IntTsu = intMatriculaTsu;
            IntLicenciatura = intMatriculaLicenciatura;
            IntPostgrado = intMatriculaPosgrado;
            DecimalTazaDesercion = decTasaDesercion;
            DecimalTazaReprobacion = decTasaReprobacion;
            DecimalTazaEficienciaTerminal = decTasaEficienciaTerminal;
            DateTimeFechaRegistro = dateTimeFechaRegistro;
            IdUsuarioRegistro = idUsuarioRegistro;
            BitActivo = bitActivo;
        }

        #endregion
    }
}
