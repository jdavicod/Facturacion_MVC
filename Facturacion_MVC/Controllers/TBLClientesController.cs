using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Facturacion_MVC.Models;

namespace Facturacion_MVC.Controllers
{
    public class TBLClientesController : Controller
    {
        private readonly FacturacionContext _context;

        public TBLClientesController(FacturacionContext context)
        {
            _context = context;
        }

        // GET: /TBLClientes
        public IActionResult Index()
        {
            return View();
        }

        // GET: /TBLClientes/GetAll
        [HttpGet]
        [Route("TBLClientes/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _context.Tblclientes
                    .Select(c => new
                    {
                        IdCliente = c.IdCliente,
                        StrNombre = c.StrNombre,
                        NumDocumento = c.NumDocumento,
                        StrDireccion = c.StrDireccion,
                        StrTelefono = c.StrTelefono,
                        StrEmail = c.StrEmail,
                        Activo = c.Activo
                    })
                    .ToListAsync();

                return Json(new { data = list });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al obtener los clientes: {ex.Message}" });
            }
        }

        // POST: /TBLClientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Tblcliente model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Modelo inválido" });

            try
            {
                model.DtmFechaModifica = DateTime.UtcNow;
                model.StrUsuarioModifica = User?.Identity?.Name ?? "system";

                _context.Add(model);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cliente agregado correctamente", data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al agregar: {ex.Message}" });
            }
        }

        // POST: /TBLClientes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Tblcliente model)
        {
            if (model == null || model.IdCliente == 0)
                return Json(new { success = false, message = "Id inválido" });

            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Modelo inválido" });

            var entity = await _context.Tblclientes.FindAsync(model.IdCliente);
            if (entity == null)
                return Json(new { success = false, message = "Cliente no encontrado" });

            try
            {
                entity.StrNombre = model.StrNombre;
                entity.NumDocumento = model.NumDocumento;
                entity.StrDireccion = model.StrDireccion;
                entity.StrTelefono = model.StrTelefono;
                entity.StrEmail = model.StrEmail;
                entity.Activo = model.Activo;
                entity.DtmFechaModifica = DateTime.UtcNow;
                entity.StrUsuarioModifica = User?.Identity?.Name ?? "system";

                _context.Update(entity);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cliente actualizado correctamente", data = entity });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al actualizar: {ex.Message}" });
            }
        }

        // POST: /TBLClientes/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Tblclientes.FindAsync(id);
            if (entity == null)
                return Json(new { success = false, message = "Cliente no encontrado" });

            try
            {
                _context.Tblclientes.Remove(entity);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cliente eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al eliminar: {ex.Message}" });
            }
        }
    }
}