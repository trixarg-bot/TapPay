using SQLite;

namespace TapPay.Models
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int ClienteId { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo_electronico { get; set; } = string.Empty;
        public string Telefono { get; set; }   = string.Empty;

        public decimal monto {get; set;}

        public int usuario_id{get; set;}
        
    }
}