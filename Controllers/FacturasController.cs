using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order2Go.Models;
using Order2Go.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Order2Go.Controllers
{
    public class FacturasController : Controller
    {
        private readonly ApplicationDbContext _context;
        readonly SqlConnection con;
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        List<Factura> ListaFacturas = new List<Factura>();

        
        public FacturasController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("Ubuntu");
        }
        public IActionResult VentasMes()
        {
            ViewData["comercios"] = "";
            if (Constantes.com == 0)
            {
                var comer = _context.Comercios.ToList();
                ViewData["comercios"] = comer;
                return View();
            }
            else
            {
               List<Comercio> Consulta = (from b in _context.Comercios where (b.Id == Constantes.com) select b).ToList();         // _context.Comercios.ToList(x => x.id == int.Parse(Constantes.com));
                ViewData["comercios"] = Consulta;
                return View();
            }
            
        }


       
        [HttpGet]
        public async Task<IActionResult> ConsultaVentas( int? id)
        {
                var NombreComercio = _context.Comercios.Find(id);
                if (id == 0)
                {
                    var i = await _context.Facturas.ToListAsync();
                    return View(i);
                }
                else
                {
                    var Consulta = from b in _context.Facturas where b.ComercioID.Contains(id.ToString()) select b;
                    ViewData["Comercio"] = NombreComercio.Nombre.ToString();

                    return View(Consulta);
                }
                 
        }


        public List<Factura> Ventas(string Mes, String Año,string Comercio)
        {
            if (ListaFacturas.Count > 0)
            {
                ListaFacturas.Clear();
            }
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM Facturas where mes=" + "'" + Mes + "'" + " and year=" + "'" + Año + "'" + " and comercioid=" + "'" + Comercio + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                ListaFacturas.Add(new Factura()
                {
                    IdFactura = int.Parse(dr["idFactura"].ToString()),
                    NombreFactura = dr["NombreFactura"].ToString(),
                    TipoVenta = dr["TipoVenta"].ToString(),
                    Monto = double.Parse(dr["Monto"].ToString()),
                    Fecha = dr["Fecha"].ToString()
                });
            }
            con.Close();
            return ListaFacturas;

        }

    }
}
