using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Order2Go.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }
        public string NombreFactura { get; set; }

        public string TipoVenta { get; set; }   //Efectivo  /  Tarjeta
        [Required(ErrorMessage = "El monto es obligatorio")]
        [Column(TypeName = "decimal(18, 2)")]
        public double Monto { get; set; }

        //[DataType(DataType.Date)]
        //[Display(Name = "Fecha")]
        [Required(ErrorMessage = "La Fecha es obligatorio")]
        public string Fecha { get; set; }
        public string Mes { get; set; }
        public string year { get; set; }
        public string ComercioID { get; set; }
    }
}
