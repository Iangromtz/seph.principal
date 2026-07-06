namespace Seph.Principal.Domain.Entities
{
    public class CatMunicipio
    {
        public long Id { get; set; }

        public string StrValor { get; set; } = string.Empty;

        public string StrDescripcion { get; set; } = string.Empty;

        #region Constructor
        public CatMunicipio()
        {

        }

        public CatMunicipio(long id, string strValor, string strDescripcion)
        {
            Id = id;
            StrValor = strValor;
            StrDescripcion = strDescripcion;
        }

        public CatMunicipio(string strValor)
        {
            StrValor = strValor;
        }
        #endregion
    }
}
