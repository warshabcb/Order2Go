using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Order2Go.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El codigo es obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La Descripcion es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La Cantidad es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El Precio es obligatorio")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen Producto")]
        public string UrlImagen { get; set; }
        public string ComercioID { get; set; }
    }
}
