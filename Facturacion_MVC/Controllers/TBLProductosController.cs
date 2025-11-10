using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion_MVC.Models;

namespace Facturacion_MVC.Controllers
{
    public class TBLProductosController : Controller
    {
        // LISTAR PRODUCTOS
        public IActionResult Index()
        {
            using var db = new FacturacionContext();
            var productos = db.Tblproductos
                .Include(p => p.IdCategoriaNavigation)
            .ToList(); return View(productos);
        }

        // GET: Mostrar formulario
        [HttpGet]
        public IActionResult Nuevo()
        {
            FacturacionContext db = new FacturacionContext();
            ViewBag.IdCategoria = new SelectList(db.TblcategoriaProds, "IdCategoria", "StrDescripcion");
            return View();
        }

        // POST: Guardar producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Nuevo(Tblproducto producto)
        {
            try
            {
                // Inicializar valores automáticos
                producto.DtmFechaModifica = DateTime.Now;
                producto.StrUsuarioModifica = string.IsNullOrWhiteSpace(producto.StrUsuarioModifica)
                    ? Environment.UserName
                    : producto.StrUsuarioModifica;

                // Quitar propiedades no enviadas
                ModelState.Remove(nameof(producto.IdCategoriaNavigation));
                ModelState.Remove(nameof(producto.StrUsuarioModifica));

                // Si el modelo no es válido, se vuelve a mostrar el formulario con errores
                if (!ModelState.IsValid)
                {
                    FacturacionContext db = new FacturacionContext();
                    ViewBag.IdCategoria = new SelectList(db.TblcategoriaProds, "IdCategoria", "StrDescripcion", producto.IdCategoria);
                    return View(producto);
                }

                // Guardar en la base de datos
                using var dbContext = new FacturacionContext();
                dbContext.Tblproductos.Add(producto);
                dbContext.SaveChanges();

                // Mensaje de éxito temporal
                TempData["SuccessMessage"] = "✅ Producto agregado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar: " + (ex.GetBaseException()?.Message ?? ex.Message));
                FacturacionContext db = new FacturacionContext();
                ViewBag.IdCategoria = new SelectList(db.TblcategoriaProds, "IdCategoria", "StrDescripcion", producto.IdCategoria);
                return View(producto);
            }
        }


        public ActionResult Editar(int id)
        {
            FacturacionContext db = new FacturacionContext();

            var datoProducto = db.Tblproductos.Find(id);

            FacturacionContext db2 = new FacturacionContext();
            ViewBag.IdCategoria = new SelectList(db2.TblcategoriaProds, "IdCategoria", "StrDescripcion", datoProducto.IdCategoria);

            return View(datoProducto);
        }

        [HttpPost]
        public ActionResult Editar(Tblproducto productoEditado)
        {
            try
            {
                FacturacionContext db = new FacturacionContext();
                var productoOriginal = db.Tblproductos.Find(productoEditado.IdProducto);
                if (productoOriginal != null)
                {
                    productoOriginal.StrNombre = productoEditado.StrNombre;
                    productoOriginal.StrCodigo = productoEditado.StrCodigo;
                    productoOriginal.NumPrecioCompra = productoEditado.NumPrecioCompra;
                    productoOriginal.NumPrecioVenta = productoEditado.NumPrecioVenta;
                    productoOriginal.IdCategoria = productoEditado.IdCategoria;
                    productoOriginal.StrDetalle = productoEditado.StrDetalle;
                    productoOriginal.StrFoto = productoEditado.StrFoto;
                    productoOriginal.NumStock = productoEditado.NumStock;
                    productoOriginal.DtmFechaModifica = DateTime.Now;
                    productoOriginal.StrUsuarioModifica = string.IsNullOrWhiteSpace(productoEditado.StrUsuarioModifica) ? Environment.UserName : productoEditado.StrUsuarioModifica;
                    db.SaveChanges();

                    // Mensaje de éxito temporal
                    TempData["SuccessMessage"] = "✅ Producto editado correctamente.";

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al editar: " + ex.GetBaseException()?.Message ?? ex.Message);
                FacturacionContext db2 = new FacturacionContext();
                ViewBag.IdCategoria = new SelectList(db2.TblcategoriaProds, "IdCategoria", "StrDescripcion", productoEditado.IdCategoria);
                return View(productoEditado);
            }
        }



        [HttpGet]
        public ActionResult Borrar(int id)
        {
            try
            {
                using (FacturacionContext db = new FacturacionContext())
                {
                    var producto = db.Tblproductos.Find(id);
                    if (producto != null)
                    {
                        db.Tblproductos.Remove(producto);
                        db.SaveChanges();

                        // Mensaje de éxito temporal
                        TempData["SuccessMessage"] = "✅ Producto eliminado correctamente.";
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}