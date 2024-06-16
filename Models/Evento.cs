using SQLite;

namespace TapPay.Models
{
    public class Evento
    {
        [PrimaryKey]
        public int EventoId { get; set; }
        
        public string Nombre { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public int OrganizadorId { get; set; }
    }
}
