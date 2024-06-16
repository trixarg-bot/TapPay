using SQLite;

namespace TapPay.Models
{
    public class TarjetaNFC
    {
        [PrimaryKey, AutoIncrement]
        public int TarjetaId { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int ClienteId { get; set; }
    }
}
