using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal Precio { get; set; }

        public int Stock { get; set; }
    }
}
