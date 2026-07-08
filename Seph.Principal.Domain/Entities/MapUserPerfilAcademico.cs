namespace Seph.Principal.Domain.Entities
{
    public class MapUserPerfilAcademico
    {
        public long Id { get; set; }
        public Guid IdAspNetUsers { get; set; }
        public long IdCatPerfilAcademico { get; set; }

        public MapUserPerfilAcademico() { }

        public MapUserPerfilAcademico(Guid idAspNetUsers, long idCatPerfilAcademico)
        {
            IdAspNetUsers = idAspNetUsers;
            IdCatPerfilAcademico = idCatPerfilAcademico;
        }
    }
}
