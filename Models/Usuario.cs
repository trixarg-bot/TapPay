using SQLite;

namespace TapPay.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int UsuarioId { get; set; }
        public string Cedula {get; set;} = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;

        
    }
}
