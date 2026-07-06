using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    /// <summary>
    /// Relación N:M entre Empleado y CatPerfilAcademico.
    /// Un empleado puede tener uno o varios perfiles académicos (o ninguno).
    /// </summary>
    public class MapEmpleadoPerfilAcademico
    {
        public long Id { get; set; }

        public long IdEmpleado { get; set; }

        public long IdCatPerfilAcademico { get; set; }

        #region Constructor
        public MapEmpleadoPerfilAcademico()
        {

        }

        public MapEmpleadoPerfilAcademico(long idEmpleado, long idCatPerfilAcademico)
        {
            IdEmpleado = idEmpleado;
            IdCatPerfilAcademico = idCatPerfilAcademico;
        }
        #endregion
    }
}
