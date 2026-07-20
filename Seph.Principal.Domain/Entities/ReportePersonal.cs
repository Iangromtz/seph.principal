using System;


namespace Seph.Principal.Domain.Entities
{
    public class ReportePersonal
    {
        public long Id { get; set; }

        public long IdMapInstitucionPeriodo { get; set; }

        public int IntTotalGeneral { get; set; }

        public int IntTotalDirectivos { get; set; }

        public int IntTotalDirectivosHombres { get; set; }

        public int IntTotalDirectivosMujeres { get; set; }

        public int IntTotalAdministrativos { get; set; }

        public int IntTotalAdministrativosHombres { get; set; }

        public int IntTotalAdministrativosMujeres { get; set; }

        public int IntTotalDocentes { get; set; }

        public int IntTotalDocentesHombres { get; set; }

        public int IntTotalDocentesMujeres { get; set; }

        public int IntTotalDocentesTiempoCompleto { get; set; }

        public int IntTotalDocentesAsignatura { get; set; }

        public int IntTotalDocentesHora { get; set; }

        public long IdNivelAcademico { get; set; }

        public DateTime DateTimeFechaRegistro { get; set; }

        public Guid IdUsuarioRegistro { get; set; }

        public bool BitActivo { get; set; }

        #region Constructor

        public ReportePersonal()
        {

        }

        public ReportePersonal(
            long id,
            long idMapInstitucionPeriodo,
            int intTotalGeneral,
            int intTotalDirectivos,
            int intTotalDirectivosHombres,
            int intTotalDirectivosMujeres,
            int intTotalAdministrativos,
            int intTotalAdministrativosHombres,
            int intTotalAdministrativosMujeres,
            int intTotalDocentes,
            int intTotalDocentesHombres,
            int intTotalDocentesMujeres,
            int intTotalDocentesTiempoCompleto,
            int intTotalDocentesAsignatura,
            int intTotalDocentesHora,
            long idNivelAcademico,
            DateTime dateTimeFechaRegistro,
            Guid idUsuarioRegistro,
            bool bitActivo)
        {
            Id = id;
            IdMapInstitucionPeriodo = idMapInstitucionPeriodo;
            IntTotalGeneral = intTotalGeneral;
            IntTotalDirectivos = intTotalDirectivos;
            IntTotalDirectivosHombres = intTotalDirectivosHombres;
            IntTotalDirectivosMujeres = intTotalDirectivosMujeres;
            IntTotalAdministrativos = intTotalAdministrativos;
            IntTotalAdministrativosHombres = intTotalAdministrativosHombres;
            IntTotalAdministrativosMujeres = intTotalAdministrativosMujeres;
            IntTotalDocentes = intTotalDocentes;
            IntTotalDocentesHombres = intTotalDocentesHombres;
            IntTotalDocentesMujeres = intTotalDocentesMujeres;
            IntTotalDocentesTiempoCompleto = intTotalDocentesTiempoCompleto;
            IntTotalDocentesAsignatura = intTotalDocentesAsignatura;
            IntTotalDocentesHora = intTotalDocentesHora;
            IdNivelAcademico = idNivelAcademico;
            DateTimeFechaRegistro = dateTimeFechaRegistro;
            IdUsuarioRegistro = idUsuarioRegistro;
            BitActivo = bitActivo;
        }

        #endregion
    }
}