using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        [Display(Name = "Nombre del producto")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 100000.00, ErrorMessage = "El precio maximo es 100000.00 $")]
        [Display(Name = "Precio del producto (USD)")]
        [Precision(8, 2)]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es requerido")]
        [Range(0, 100000, ErrorMessage = "El stock maximo es 100000 unidades")]
        [Display(Name = "Stock del producto")]
        [Precision(6, 0)]
        public int Stock { get; set; }
    }
}
