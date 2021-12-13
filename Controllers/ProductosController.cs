using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Order2Go.Data;
using Order2Go.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Order2Go.Extensions;

namespace MiniSuper.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
       
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductosController(ApplicationDbContext context , IWebHostEnvironment hostingEnvironmen)
        {
            _context = context;
          
            _hostingEnvironment = hostingEnvironmen;
        }

        public Producto BuscarProducto (string codigo)
        {
            Producto data = _context.Productos.SingleOrDefault(x=>x.Codigo == codigo);

            return  data;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (Constantes.com == 0)
            {
                return View(await _context.Productos.ToListAsync());
            }
            else
            {
                var Consulta = from b in _context.Productos where b.ComercioID.Contains(Constantes.com.ToString()) select b;

                return View( Consulta);
            }
           
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var BuscarProducto = _context.Productos.Find(id);
            if (BuscarProducto == null)
            {
                return NotFound();
            }
            return View(BuscarProducto);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var BuscarProducto = _context.Productos.Find(id);
            if (BuscarProducto == null)
            {
                return NotFound();
            }
            return View(BuscarProducto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Producto producto)
        {      
            if (ModelState.IsValid)
            {
                string RutaPrincipal = _hostingEnvironment.WebRootPath;
                var Archivos = HttpContext.Request.Form.Files;
                if (Archivos.Count() > 0)
                {
                    var Idproducto = producto.Id;
                    //Producto  ArticuloDesdeDb = _context.Productos.FirstOrDefault(x => x.Id == Idproducto);
                    Producto ArticuloDesdeDb = _context.Productos.Find(Idproducto);

                    string nombreArchivo = Guid.NewGuid().ToString();
                    var Subidas = Path.Combine(RutaPrincipal, @"img\productos");
                    var Extension = Path.GetExtension(Archivos[0].FileName);
                    var rutaImagen = "";
                    if (ArticuloDesdeDb.UrlImagen != null)
                    {
                        rutaImagen = Path.Combine(RutaPrincipal, ArticuloDesdeDb.UrlImagen.TrimStart('\\'));
                    }
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    using (var fileStreams = new FileStream(Path.Combine(Subidas, nombreArchivo + Extension), FileMode.Create))
                    {
                        Archivos[0].CopyTo(fileStreams);
                    }
                    ArticuloDesdeDb.Codigo = producto.Codigo;
                    ArticuloDesdeDb.Precio = producto.Precio;
                    ArticuloDesdeDb.Cantidad = producto.Cantidad;
                    ArticuloDesdeDb.Nombre = producto.Nombre;
                    ArticuloDesdeDb.Descripcion = producto.Descripcion;
                    ArticuloDesdeDb.ComercioID = Constantes.com.ToString();

                    ArticuloDesdeDb.UrlImagen = @"\img\productos\" + nombreArchivo + Extension;
                    //producto.UrlImagen = @"\img\productos\" + nombreArchivo + Extension;
                    _context.Update(ArticuloDesdeDb);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var Idproducto = producto.Id;
                    Producto ArticuloDesdeDb = _context.Productos.Find(Idproducto);
                    ArticuloDesdeDb.Codigo = producto.Codigo;
                    ArticuloDesdeDb.Precio = producto.Precio;
                    ArticuloDesdeDb.Cantidad = producto.Cantidad;
                    ArticuloDesdeDb.Nombre = producto.Nombre;
                    ArticuloDesdeDb.Descripcion = producto.Descripcion;
                    ArticuloDesdeDb.ComercioID = Constantes.com.ToString();
                    ArticuloDesdeDb.UrlImagen = ArticuloDesdeDb.UrlImagen;
                    _context.Update(ArticuloDesdeDb);
                    await _context.SaveChangesAsync();
                }
                 return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                string RutaPrincipal = _hostingEnvironment.WebRootPath;
                var Archivos =  HttpContext.Request.Form.Files;
                string NombreArchivo = Guid.NewGuid().ToString();
                var Subidas = Path.Combine(RutaPrincipal, @"img\productos");
                var ExtensionArchivo = Path.GetExtension(Archivos[0].FileName);
                using (var fileStreams = new FileStream(Path.Combine(Subidas, NombreArchivo + ExtensionArchivo), FileMode.Create))
                {
                  Archivos[0].CopyTo(fileStreams);
                }
                producto.UrlImagen = @"\img\productos\" + NombreArchivo + ExtensionArchivo;
                producto.ComercioID = Constantes.com.ToString();
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFisico(int? id)
        {
            var BuscarProducto = await _context.Productos.FindAsync(id);
            string RutaPrincipal = _hostingEnvironment.WebRootPath;
            if (BuscarProducto == null)
            {
                return View();
            }
            var Archivo = BuscarProducto.UrlImagen;
            var Subidas = Path.Combine(RutaPrincipal, @"img\productos");
            var RutaImagen = Path.Combine(RutaPrincipal, BuscarProducto.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(RutaImagen))
            {
                System.IO.File.Delete(RutaImagen);
            }
                
            _context.Productos.Remove(BuscarProducto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var BuscarProducto = _context.Productos.Find(id);           
            if (BuscarProducto == null)
            {
                return NotFound();
            }
            return View(BuscarProducto);
        }

        [HttpPost]                  //Codigo =  Codigo a Buscar
        public void ActualizaInventario(string Codigo,int Cantidad)
        {
            Producto pro = new Producto();
            pro = _context.Productos.FirstOrDefault(x=>x.Codigo == Codigo);
            pro.Cantidad = (pro.Cantidad - Cantidad);
            _context.Productos.Update(pro);
            _context.SaveChanges();       
        }
         public IActionResult GetProductos(int? idproducto)
        {
            if (idproducto != null)
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("CARRITO") == null)
                {
                    carrito = new List<int>();
                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                }
                if (carrito.Contains(idproducto.Value) == false)
                {
                    carrito.Add(idproducto.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);
                }
            }
            List<Producto> productos = _context.Productos.ToList();
            return View(productos);
        }
          public IActionResult Carrito(int? idproducto)
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            if (carrito == null)
            {
                return View();
            }
            else
            {
                if (idproducto != null)
                {
                    carrito.Remove(idproducto.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);
                }
                List<Producto> productos = GetProductosCarrito(carrito);
                return View(productos);
            }
        }
        public IActionResult Pedidos()
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            List<Producto> productos = _context.Productos.ToList();
            HttpContext.Session.Remove("CARRITO");
            return View(productos);
        }
        public List<Producto> GetProductosCarrito(List<int> idproductos)
        {
            List<Producto> productos = _context.Productos.Where(xx => idproductos.Contains(xx.Id)).ToList();             
            return productos;
        }

    }
}
