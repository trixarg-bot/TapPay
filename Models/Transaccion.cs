using SQLite;

namespace TapPay.Models
{
    public class Transaccion
    {
        [PrimaryKey, AutoIncrement]
        public int TransaccionId { get; set; }
        
        public int TarjetaId { get; set; }
        public int ProductoId { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; } = string.Empty;

        public int Cantidad {get; set;}
    }
}
