using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Facturacion_MVC.Models;

namespace Facturacion_MVC.Controllers
{
    // Controlador para operaciones CRUD de clientes
    public class TBLClientesController : Controller
    {
        private readonly FacturacionContext _db;

        // Inyección del contexto de datos
        public TBLClientesController(FacturacionContext db)
        {
            _db = db;
        }

        // DTO limpio para el front
        public record ClientDto(
            int idCliente,
            string? strNombre,
            long? numDocumento,
            string? strDireccion,
            string? strTelefono,
            string? strEmail,
            bool activo
        );

        // Página principal
        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Obtener todos los clientes y mapear a DTO
            var data = await _db.Tblclientes
                .Select(c => new ClientDto(
                    c.IdCliente,
                    c.StrNombre,
                    c.NumDocumento,
                    c.StrDireccion,
                    c.StrTelefono,
                    c.StrEmail,
                    c.Activo
                ))
                .ToListAsync();

            // Devolver JSON para DataTables
            return Json(new { data });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClientDto dto)
        {
            // Validación: nombre obligatorio
            if (string.IsNullOrWhiteSpace(dto.strNombre))
                return Json(new { success = false, message = "El nombre es obligatorio." });

            // Validación: email (si se proporcionó)
            if (!string.IsNullOrWhiteSpace(dto.strEmail) && !new EmailAddressAttribute().IsValid(dto.strEmail))
                return Json(new { success = false, message = "Correo inválido." });

            // Validación: teléfono solo dígitos (si se proporcionó)
            if (!string.IsNullOrWhiteSpace(dto.strTelefono) && !dto.strTelefono.All(char.IsDigit))
                return Json(new { success = false, message = "El teléfono debe contener solo números." });

            // Crear entidad y poblarla desde DTO
            var entity = new Tblcliente
            {
                StrNombre = dto.strNombre?.Trim(),
                NumDocumento = dto.numDocumento,
                StrDireccion = dto.strDireccion?.Trim(),
                StrTelefono = dto.strTelefono?.Trim(),
                StrEmail = dto.strEmail?.Trim(),
                Activo = dto.activo,
                DtmFechaModifica = DateTime.UtcNow,
                StrUsuarioModifica = User?.Identity?.Name ?? "sistema"
            };

            // Agregar y guardar en BD
            _db.Tblclientes.Add(entity);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Cliente creado correctamente.", id = entity.IdCliente });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] ClientDto dto)
        {
            // Buscar entidad existente
            var entity = await _db.Tblclientes.FindAsync(dto.idCliente);
            if (entity == null)
                return Json(new { success = false, message = "Cliente no encontrado." });

            // Validación: nombre obligatorio
            if (string.IsNullOrWhiteSpace(dto.strNombre))
                return Json(new { success = false, message = "El nombre es obligatorio." });

            // Validación: email (si se proporcionó)
            if (!string.IsNullOrWhiteSpace(dto.strEmail) && !new EmailAddressAttribute().IsValid(dto.strEmail))
                return Json(new { success = false, message = "Correo inválido." });

            // Validación: teléfono solo dígitos (si se proporcionó)
            if (!string.IsNullOrWhiteSpace(dto.strTelefono) && !dto.strTelefono.All(char.IsDigit))
                return Json(new { success = false, message = "El teléfono debe contener solo números." });

            // Actualizar campos y guardar
            entity.StrNombre = dto.strNombre?.Trim();
            entity.NumDocumento = dto.numDocumento;
            entity.StrDireccion = dto.strDireccion?.Trim();
            entity.StrTelefono = dto.strTelefono?.Trim();
            entity.StrEmail = dto.strEmail?.Trim();
            entity.Activo = dto.activo;
            entity.DtmFechaModifica = DateTime.UtcNow;
            entity.StrUsuarioModifica = User?.Identity?.Name ?? "sistema";

            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Cambios guardados correctamente." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            // Buscar y eliminar entidad
            var entity = await _db.Tblclientes.FindAsync(id);
            if (entity == null)
                return Json(new { success = false, message = "Cliente no encontrado." });

            _db.Tblclientes.Remove(entity);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Cliente eliminado correctamente." });
        }
    }
}
