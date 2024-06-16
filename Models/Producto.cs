using SQLite;

namespace TapPay.Models
{
    public class Producto
    {
        [PrimaryKey]
        public int ProductoId { get; set; }
        
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
    }
}
