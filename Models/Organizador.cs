using SQLite;

namespace TapPay.Models
{
    public class Organizador
    {
         [PrimaryKey]
        public int OrganizadorId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string rnc  { get; set; } = string.Empty;

        public int usuario_id {get; set;}
    }
}
