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
            var productos = db.Tblproductos.ToList();
            return View(productos);
        }

        // GET: Mostrar formulario
        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        // POST: Guardar producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Nuevo(Tblproducto producto)
        {
            try
            {
                // Rellenos obligatorios del servidor antes de validar
                producto.DtmFechaModifica = DateTime.Now;
                producto.StrUsuarioModifica = string.IsNullOrWhiteSpace(producto.StrUsuarioModifica) ? Environment.UserName : producto.StrUsuarioModifica;
                //producto.StrCodigo = string.IsNullOrWhiteSpace(producto.StrCodigo) ? "12" : producto.StrCodigo;
                producto.NumPrecioCompra ??= 0;

                // Eliminar propiedades de navegación del ModelState si no vienen en el formulario
                ModelState.Remove(nameof(producto.IdCategoriaNavigation));
                ModelState.Remove(nameof(producto.StrUsuarioModifica));
                //ModelState.Remove(nameof(producto.StrCodigo));

                if (!ModelState.IsValid)
                {
                    // Recolectar errores de ModelState de forma segura
                    var erroresAntes = ModelState
                        .Where(kvp => kvp.Value != null && kvp.Value.Errors != null && kvp.Value.Errors.Count > 0)
                        .Select(kvp => new
                        {
                            Key = kvp.Key,
                            Errors = kvp.Value!.Errors
                                .Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? (e.Exception?.Message ?? "Error desconocido") : e.ErrorMessage)
                                .ToArray()
                        })
                        .ToList();

                    ViewBag.ModelErrors = erroresAntes;
                    return View(producto);
                }

                using var db = new FacturacionContext();

                db.Tblproductos.Add(producto);

                try
                {
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    // Mostrar detalle de la excepción interna (útil para depuración)
                    var detalle = dbEx.GetBaseException()?.Message ?? dbEx.Message;
                    ModelState.AddModelError(string.Empty, "Error al guardar (DB): " + detalle);

                    var erroresDb = ModelState
                        .Where(kvp => kvp.Value != null && kvp.Value.Errors != null && kvp.Value.Errors.Count > 0)
                        .Select(kvp => new
                        {
                            Key = kvp.Key,
                            Errors = kvp.Value!.Errors.Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? (e.Exception?.Message ?? "Error desconocido") : e.ErrorMessage).ToArray()
                        }).ToList();

                    ViewBag.ModelErrors = erroresDb;
                    return View(producto);
                }
            }
            catch (Exception ex)
            {
                var baseMsg = ex.GetBaseException()?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, "Error al guardar: " + baseMsg);

                var errores = ModelState
                    .Where(kvp => kvp.Value != null && kvp.Value.Errors != null && kvp.Value.Errors.Count > 0)
                    .Select(kvp => new
                    {
                        Key = kvp.Key,
                        Errors = kvp.Value!.Errors.Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? (e.Exception?.Message ?? "Error desconocido") : e.ErrorMessage).ToArray()
                    })
                    .ToList();

                ViewBag.ModelErrors = errores;
                return View(producto);
            }
        }
    }
}
